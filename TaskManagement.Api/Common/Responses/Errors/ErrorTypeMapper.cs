using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Api.Common.Responses.Errors
{
    public static class ErrorTypeMapper
    {
        private static readonly Dictionary<ErrorType, (int code, string title)> Map = new()
        {
            [ErrorType.Validation] = (StatusCodes.Status400BadRequest, "Validation Error"),
            [ErrorType.Unauthorized] = (StatusCodes.Status401Unauthorized, "Unauthorized"),
            [ErrorType.Forbidden] = (StatusCodes.Status403Forbidden, "Forbidden"),
            [ErrorType.NotFound] = (StatusCodes.Status404NotFound, "Resource Not Found"),
            [ErrorType.Conflict] = (StatusCodes.Status409Conflict, "Conflict Error"),
        };
        public static (int code, string title) Resolve(ErrorType errorType)
        {
            return Map.TryGetValue(errorType, out var value) ? value
                : (StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }
}
