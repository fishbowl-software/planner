using System.Text.Json.Serialization;

namespace FishbowlSoftware.Planner.Domain.Core
{
    public class AuditableEntity : Entity
    {
        [JsonIgnore]
        public string? CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public string? UpdatedBy { get; set; }

        [JsonIgnore]
        public DateTime? UpdatedDate { get; set; }
    }
}
