using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Identity;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FishbowlSoftware.Planner.Application.Behaviours
{
    internal sealed class SetUserContextBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IUserAuthorizedRequest<TResponse>
        where TResponse : IResult, new()
    {
        private readonly AuthorizedUserContext _authorizedUserContext;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<SetUserContextBehaviour<TRequest, TResponse>> _logger;

        public SetUserContextBehaviour(
            AuthorizedUserContext authorizedUserContext,
            IUnitOfWork uow,
            ILogger<SetUserContextBehaviour<TRequest, TResponse>> logger)
        {
            _authorizedUserContext = authorizedUserContext;
            _uow = uow;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var userExists = await _uow.Repository<User>()
                .AnyAsync(i => i.Id == request.UserId || i.Email == request.UserId || i.UserName == request.UserId);

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

            _authorizedUserContext.UserId = request.UserId;
            return await next();
        }
    }
}
