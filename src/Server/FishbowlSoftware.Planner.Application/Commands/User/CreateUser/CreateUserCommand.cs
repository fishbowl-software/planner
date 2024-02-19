using FishbowlSoftware.Planner.Shared;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Commands
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
