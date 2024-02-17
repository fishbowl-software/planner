using FishbowSoftware.Planner.Application.Core;
using FishbowSoftware.Planner.Domain.Entities;
using FishbowSoftware.Planner.Domain.Persistence;
using FishbowSoftware.Planner.Mappings;
using FishbowSoftware.Planner.Shared;
using FishbowSoftware.Planner.Shared.Models;

namespace FishbowSoftware.Planner.Application.Queries
{
    internal class GetUserHandler : RequestHandler<GetUserQuery, Result<UserDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetUserHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected override async Task<Result<UserDto>> HandleValidated(
            GetUserQuery req, CancellationToken ct)
        {
            var userEntity = await _uow.Repository<User>().GetAsync(i => i.Id == req.Id);

            if (userEntity is null)
            {
                return Result<UserDto>.CreateError($"Could not find a user with ID {req.Id}");
            }

            var userDto = userEntity.ToDto(null);
            return Result<UserDto>.CreateSuccess(userDto);
        }
    }
}
