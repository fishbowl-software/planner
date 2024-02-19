using FishbowlSoftware.Planner.Shared;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Commands
{
    public class UpdateUserCommand : IRequest<Result>
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
