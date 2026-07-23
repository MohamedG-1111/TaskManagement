namespace TaskManagement.Api.Common.Responses
{
    public class ApiMeta
    {
        public string TraceId { get; init; } = string.Empty;

        public PaginationMeta? Pagination { get; init; }
    }
}
