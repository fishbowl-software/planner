﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FishbowlSoftware.Planner.Domain.Core;
using FishbowlSoftware.Planner.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class User : IdentityUser, IAuditableEntity<string>, IDomainEventHolder
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Organization { get; set; }
    public Address Address { get; set; } = new();
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    
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
