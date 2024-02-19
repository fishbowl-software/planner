using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands
{
    internal class CreateUserHandler : RequestHandler<CreateUserCommand, Result>
    {
        private readonly IUnitOfWork _uow;

        public CreateUserHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected override async Task<Result> HandleValidated(
            CreateUserCommand req, CancellationToken ct)
        {
            var newUser = User.Create(req.Email!, req.PhoneNumber!);


            await _uow.Repository<User>().AddAsync(newUser);
            await _uow.SaveChangesAsync(ct);
            return Result.CreateSuccess();
        }
    }
}
