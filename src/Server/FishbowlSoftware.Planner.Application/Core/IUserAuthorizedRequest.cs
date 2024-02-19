using MediatR;

namespace FishbowlSoftware.Planner.Application.Core
{
    internal interface IUserAuthorizedRequest<out TResult> : IRequest<TResult>
    {
        string? UserId { get; set; }
    }
}
