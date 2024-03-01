using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Events;

internal class UserCreatedEvent : IDomainEvent
{
    public required string UserId { get; set; }
    public required string Email { get; set; }
}
