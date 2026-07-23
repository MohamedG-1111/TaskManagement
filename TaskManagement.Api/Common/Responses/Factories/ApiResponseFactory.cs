using TaskManagement.Api.Common.Responses;
using TaskManagement.Domain.Common.Results;

public static class ApiResponseFactory
{
    public static ApiResponse<object?> Success(
        object? data,
        string traceId,
        string? message = null)
    {
        if (data is IPaginatedResult paged)
        {
            return new ApiResponse<object?>
            {
                Data = paged.Items,
                IsSuccess = true,
                Message = message,
                Meta = new ApiMeta
                {
                    TraceId = traceId,
                    Pagination = new PaginationMeta(
                        paged.PageNumber,
                        paged.PageSize,
                        paged.TotalCount)
                }
            };
        }

        return new ApiResponse<object?>
        {
            Data = data,
            IsSuccess = true,
            Message = message,
            Meta = new ApiMeta
            {
                TraceId = traceId
            }
        };
    }
}