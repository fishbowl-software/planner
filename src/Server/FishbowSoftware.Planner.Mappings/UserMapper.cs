using FishbowSoftware.Planner.Domain.Entities;
using FishbowSoftware.Planner.Shared.Models;

namespace FishbowSoftware.Planner.Mappings
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User entity, IEnumerable<string>? roles)
        {
            var dto = new UserDto
            {
                Id = entity.Id,
                Roles = roles ?? Array.Empty<string>(),
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
            };
            return dto;
        }
    }
}
