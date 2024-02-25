namespace FishbowlSoftware.Planner.Shared.Models;

public record ClientDto
{
    public string? Id { get; set; }
    public string? AccountId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Organization { get; set; }
    public AddressDto Address { get; set; } = new();
}
