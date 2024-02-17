using FishbowSoftware.Planner.Application.Core;
using FishbowSoftware.Planner.Domain.Entities;
using FishbowSoftware.Planner.Domain.Persistence;
using FishbowSoftware.Planner.Shared;

namespace FishbowSoftware.Planner.Application.Commands
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
