namespace TaskManagement.Domain.Common.Results
{
    public class Error
    {
        public string Code { get; set; } = null!;
        public string Message { get; set; } = null!;

        public ErrorType Type { get; set; }

        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public static Error Validation(string code, string message)
               => new(code, message, ErrorType.Validation);

        public static Error NotFound(string code, string message)
            => new(code, message, ErrorType.NotFound);

        public static Error Conflict(string code, string message)
            => new(code, message, ErrorType.Conflict);

        public static Error Unauthorized(string code, string message)
            => new(code, message, ErrorType.Unauthorized);

        public static Error Forbidden(string code, string message)
            => new(code, message, ErrorType.Forbidden);

        public static Error Failure(string code, string message)
            => new(code, message, ErrorType.Failure);


    }
}
