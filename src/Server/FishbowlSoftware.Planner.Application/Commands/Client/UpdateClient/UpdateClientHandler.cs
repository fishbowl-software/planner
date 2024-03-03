using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class UpdateClientHandler : RequestHandler<UpdateClientCommand, Result>
{
    private readonly IUnitOfWork _uow;

    public UpdateClientHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    protected override async Task<Result> HandleValidated(
        UpdateClientCommand req, CancellationToken ct)
    {
        var client = await _uow.Repository<Client>()
            .GetAsync(i => i.Id == req.Id, false);

        if (client is null)
        {
            return Result.CreateError($"Could not find a client with ID {req.Id}");
        }

        if (!string.IsNullOrEmpty(req.Name) && req.Name != client.Name)
        {
            client.Name = req.Name;
        }

        if (!string.IsNullOrEmpty(req.UserId) && req.UserId != client.UserId)
        {
            await UpdateClientUserIdAsync(client, req.UserId);
        }

        _uow.Repository<Client>().Update(client);
        await _uow.SaveChangesAsync(ct);
        return Result.CreateSuccess();
    }
    
    private async Task UpdateClientUserIdAsync(Client client, string? userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            client.User = null;
            client.UserId = null;
        }
        else
        {
            var user = await _uow.Repository<User>().GetAsync(i => i.Id == userId);
            
            if (user is null)
            {
                throw new InvalidOperationException($"Could not find a user with ID {userId}");
            }

            client.User = user;
            client.UserId = user.Id;
        }
    }
}
