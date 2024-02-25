using FishbowlSoftware.Planner.Domain.Core;
using Microsoft.Extensions.Logging;

namespace FishbowlSoftware.Planner.Domain.Events;

internal class UserCreatedHandler : IDomainEventHandler<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedHandler> _logger;

    public UserCreatedHandler(ILogger<UserCreatedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Client created with id {Client} and email {Email}", notification.UserId, notification.Email);
        return Task.CompletedTask;
    }
}
