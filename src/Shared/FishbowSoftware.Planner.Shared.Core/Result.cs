using System.Text.Json.Serialization;

namespace FishbowSoftware.Planner.Shared
{
    public class Result : IResult
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Error { get; init; }
        public bool IsSuccess => string.IsNullOrEmpty(Error);

        public static Result CreateSuccess() => new();
        public static Result CreateError(string error) => new() { Error = error };
    }

    public class Result<T> : Result
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public T? Data { get; init; }

        public static Result<T> CreateSuccess(T result) => new() { Data = result };
        public new static Result<T> CreateError(string error) => new() { Error = error };
    }
}
