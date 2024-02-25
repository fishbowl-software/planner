using FishbowlSoftware.Planner.Shared;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Commands;

public class UpdateClientCommand : IRequest<Result>
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Organization { get; set; }
    public string? PhoneNumber { get; set; }
}
