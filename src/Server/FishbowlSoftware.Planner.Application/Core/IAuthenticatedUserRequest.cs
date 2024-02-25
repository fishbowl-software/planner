using MediatR;

namespace FishbowlSoftware.Planner.Application.Core
{
    internal interface IAuthenticatedUserRequest<out TResult> : IRequest<TResult>
    {
        string? UserId { get; set; }
    }
}
