using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FishbowlSoftware.Planner.Domain.Core;
using FishbowlSoftware.Planner.Domain.ValueObjects;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class User : IdentityUser, IEntity<string>, IDomainEventHolder
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Organization { get; set; }
    public Address Address { get; set; } = new();
    public List<Project> Projects { get; set; } = [];
    
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
