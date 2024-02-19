using MediatR;

namespace FishbowlSoftware.Planner.Domain.Core
{
    public interface IDomainEventHandler<in T> : INotificationHandler<T> where T : IDomainEvent
    {
    }
}
