using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands
{
    internal class UpdateUserHandler : RequestHandler<UpdateUserCommand, Result>
    {
        private readonly IUnitOfWork _uow;

        public UpdateUserHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected override async Task<Result> HandleValidated(
            UpdateUserCommand req, CancellationToken ct)
        {
            var user = await _uow.Repository<User>()
                .GetAsync(i => i.Id == req.Id, false);

            if (user is null)
            {
                return Result.CreateError($"Could not find a user with ID {req.Id}");
            }

            if (!string.IsNullOrEmpty(req.Email))
            {
                user.Email = req.Email;
            }

            if (!string.IsNullOrEmpty(req.PhoneNumber))
            {
                user.PhoneNumber = req.PhoneNumber;
            }

            _uow.Repository<User>().Update(user);
            await _uow.SaveChangesAsync(ct);
            return Result.CreateSuccess();
        }
    }
}
