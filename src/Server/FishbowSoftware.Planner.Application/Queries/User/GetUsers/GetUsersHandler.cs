using FishbowSoftware.Planner.Application.Core;
using FishbowSoftware.Planner.Domain.Entities;
using FishbowSoftware.Planner.Domain.Persistence;
using FishbowSoftware.Planner.Domain.Specifications;
using FishbowSoftware.Planner.Mappings;
using FishbowSoftware.Planner.Shared;
using FishbowSoftware.Planner.Shared.Models;

namespace FishbowSoftware.Planner.Application.Queries
{
    internal class GetUsersHandler : RequestHandler<GetUsersQuery, PagedResult<UserDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetUsersHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected override async Task<PagedResult<UserDto>> HandleValidated(
            GetUsersQuery req, CancellationToken ct)
        {
            var totalItems = await _uow.Repository<User>().CountAsync();

            var users = _uow.Repository<User>()
                .ApplySpecification(new GetUsersPaged(req.OrderBy, req.Page, req.PageSize))
                .Select(i => i.ToDto(null))
                .ToArray();

            return PagedResult<UserDto>.CreateSuccess(users, totalItems, req.PageSize);
        }
    }
}
