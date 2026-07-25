using System.Text.Json.Serialization;

namespace TaskManagement.Api.Common.Responses
{

    public class ApiMeta
    {
        public string TraceId { get; init; } = string.Empty;
    }

    public class PaginatedApiMeta : ApiMeta
    {
        public PaginationMeta Pagination { get; init; } = default!;
    }
}
