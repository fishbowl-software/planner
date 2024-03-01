using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Identity;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FishbowlSoftware.Planner.Application.Behaviours;

internal sealed class SetUserDataBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IAuthenticatedUserRequest<TResponse>
    where TResponse : IResult, new()
{
    private readonly AuthenticatedUserData _authenticatedUserData;
    private readonly IUnitOfWork _uow;
    private readonly ILogger<SetUserDataBehaviour<TRequest, TResponse>> _logger;

    public SetUserDataBehaviour(
        AuthenticatedUserData authenticatedUserData,
        IUnitOfWork uow,
        ILogger<SetUserDataBehaviour<TRequest, TResponse>> logger)
    {
        _authenticatedUserData = authenticatedUserData;
        _uow = uow;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var userExists = await _uow.Repository<User>()
            .AnyAsync(i => i.Id == request.UserId || i.Email == request.UserId);

        if (!userExists)
        {
            var errorMsg = string.IsNullOrEmpty(request.UserId) ? "User ID is null" : "Invalid User ID";
            _logger.LogError("Unauthorized operation: {ErrorMessage}\nRequest: {Request}",
                errorMsg, typeof(TRequest).Name);

            return new TResponse
            {
                Error = $"Unauthorized operation: {errorMsg}."
            };
        }

        _authenticatedUserData.UserId = request.UserId;
        return await next();
    }
}
