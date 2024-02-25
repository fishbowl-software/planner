using FishbowlSoftware.Planner.Domain.Core;
using FishbowlSoftware.Planner.Domain.ValueObjects;

namespace FishbowlSoftware.Planner.Domain.Entities;

public class Client : Entity
{
    public string? AccountId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Organization { get; set; }
    public string? PhoneNumber { get; set; }
    public Address Address { get; set; } = new();
    
    public List<Project> Projects { get; set; } = [];

    public static Client Create(
        string accountId,
        string firstName,
        string lastName,
        string email,
        string? phoneNumber = null,
        string? organization = null,
        Address? address = null)
    {
        var newClient = new Client
        {
            AccountId = accountId,
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
