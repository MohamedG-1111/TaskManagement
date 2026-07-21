namespace TaskManagement.Domain.Common.Result
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error? Error { get; }

        protected Result(bool isSuccess, Error? error)
        {
            if (isSuccess && error is not null)
                throw new InvalidOperationException(
                    "A successful result cannot contain an error.");

            if (!isSuccess && error is null)
                throw new InvalidOperationException(
                    "A failed result must contain an error.");

            IsSuccess = isSuccess;
            Error = error;
        }
        public static Result Success() => new Result(true, null);
        public static Result Failure(Error error) => new Result(false, error);
    }
    public class Result<TValue> : Result
    {
        public TValue? Value { get; }

        public Result(TValue? value, bool isSuccess, Error? error) : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<TValue> Success(TValue value) => new Result<TValue>(value, true, null);

        public new static Result<TValue> Failure(Error error) => new Result<TValue>(default, false, error);



    }
}
