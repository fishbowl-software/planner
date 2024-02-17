namespace FishbowSoftware.Planner.Domain.Core
{
    public interface IDomainEventHolder
    {
        List<IDomainEvent> DomainEvents { get; }
    }
}
