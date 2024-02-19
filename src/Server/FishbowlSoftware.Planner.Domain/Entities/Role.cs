using FishbowlSoftware.Planner.Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace FishbowlSoftware.Planner.Domain.Entities
{
    public class Role : IdentityRole, IEntity<string>
    {
    }
}
