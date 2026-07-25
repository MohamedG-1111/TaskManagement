using System.Text.Json.Serialization;

namespace TaskManagement.Api.Common.Responses
{
    public class ApiMeta
    {
        public string TraceId { get; init; } = string.Empty;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PaginationMeta? Pagination { get; init; }
    }
}
