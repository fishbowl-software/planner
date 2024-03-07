namespace FishbowlSoftware.Planner.Domain.Core;

public interface IAuditableEntity<TKey> : IEntity<TKey>
{
    string? CreatedBy { get; set; }
    DateTime CreatedDate { get; set; }
    string? UpdatedBy { get; set; }
    DateTime? UpdatedDate { get; set; }
}
