using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Shared.Models;

namespace FishbowlSoftware.Planner.Mappings
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
