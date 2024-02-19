using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FishbowlSoftware.Planner.Domain.Core;

namespace FishbowlSoftware.Planner.Domain.Entities
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
