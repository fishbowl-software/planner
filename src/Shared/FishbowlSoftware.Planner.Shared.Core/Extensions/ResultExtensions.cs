namespace FishbowlSoftware.Planner.Shared
{
    public static class ResultExtensions
    {
        public static bool IsError(this IResult result)
        {
            return !result.IsSuccess && !string.IsNullOrEmpty(result.Error);
        }
    }
}
