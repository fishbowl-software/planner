namespace FishbowlSoftware.Planner.Shared.Models;

public class ClientDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public UserDto? User { get; set; }
    public DateTime CreatedDate { get; set; }
}
