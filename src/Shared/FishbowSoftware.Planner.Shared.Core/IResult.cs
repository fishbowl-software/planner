namespace FishbowSoftware.Planner.Shared
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string? Error { get; init; }
    }
}
