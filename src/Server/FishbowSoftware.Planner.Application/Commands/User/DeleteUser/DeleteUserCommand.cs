using FishbowSoftware.Planner.Shared;
using MediatR;

namespace FishbowSoftware.Planner.Application.Commands
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public string? Id { get; set; }
    }
}
