using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FishbowlSoftware.Planner.Domain.Core;
using FishbowlSoftware.Planner.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class User : IdentityUser, IEntity<string>, IDomainEventHolder
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Organization { get; set; }
    public string? PhoneNumber { get; set; }
    public Address Address { get; set; } = new();
    
    public List<Project> Projects { get; set; } = [];
    
    [NotMapped, JsonIgnore]
    public List<IDomainEvent> DomainEvents { get; } = [];

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string? phoneNumber = null,
        string? organization = null,
        Address? address = null)
    {
        var newClient = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Organization = organization,
            Address = address ?? new Address()
        };

        return newClient;
    }
}
