using MediatR;

namespace FishbowSoftware.Planner.Domain.Core
{
    public interface IDomainEventHandler<in T> : INotificationHandler<T> where T : IDomainEvent
    {
    }
}
