using MediatR;

namespace FishbowSoftware.Planner.Application.Core
{
    internal interface IUserAuthorizedRequest<out TResult> : IRequest<TResult>
    {
        string? UserId { get; set; }
    }
}
