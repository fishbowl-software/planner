using FishbowSoftware.Planner.Domain.Core;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FishbowSoftware.Planner.Domain.Entities
{
    public class User : IdentityUser, IEntity<string>, IDomainEventHolder
    {
        [NotMapped, JsonIgnore]
        public List<IDomainEvent> DomainEvents { get; } = [];

        public static User Create(string email, string phoneNumber)
        {
            var newUser = new User
            {
                Email = email,
                PhoneNumber = phoneNumber
            };

            return newUser;
        }
    }
}
