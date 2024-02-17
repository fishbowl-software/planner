using FishbowSoftware.Planner.Domain.Core;
using Microsoft.AspNetCore.Identity;

namespace FishbowSoftware.Planner.Domain.Entities
{
    public class Role : IdentityRole, IEntity<string>
    {
    }
}
