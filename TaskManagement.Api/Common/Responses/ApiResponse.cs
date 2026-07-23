
namespace TaskManagement.Api.Common.Responses;

public class ApiResponse<T>
{
    public T? Data { get; init; }

    public bool IsSuccess { get; init; } = true;

    public string? Message { get; init; }

    public ApiMeta Meta { get; init; } = new();
}