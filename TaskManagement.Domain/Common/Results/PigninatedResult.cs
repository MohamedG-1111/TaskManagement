namespace TaskManagement.Domain.Common.Results
{
    public record PaginationParameters(int PageNumber = 1, int PageSize = 8);

    public class PaginatedResult<T>
    {
        private PaginatedResult() { }

        public IReadOnlyList<T> Items { get; init; } = [];
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalCount / PageSize);

        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        public static PaginatedResult<T> Create(
            IReadOnlyList<T> items,
            int totalCount,
            PaginationParameters parameters)
        {
            return new PaginatedResult<T>
            {
                Items = items,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalCount = totalCount
            };
        }

    }
}