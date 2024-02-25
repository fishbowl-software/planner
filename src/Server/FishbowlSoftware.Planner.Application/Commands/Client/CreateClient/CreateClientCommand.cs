using FishbowlSoftware.Planner.Domain.ValueObjects;
using FishbowlSoftware.Planner.Shared;
using MediatR;

namespace FishbowlSoftware.Planner.Application.Commands;

public class CreateClientCommand : IRequest<Result>
{
    public string? AccountId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Organization { get; set; }
    public Address Address { get; set; } = new();
}
