namespace TaskManagement.Domain.Common.Results
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
        private readonly TValue? _value;

        public TValue Value =>
            IsSuccess
                ? _value!
                : throw new InvalidOperationException(
                    "Cannot access the value of a failed result.");

        public Result(TValue? value, bool isSuccess, Error? error) : base(isSuccess, error)
        {
            _value = value;
        }

        public static Result<TValue> Success(TValue value) => new Result<TValue>(value, true, null);

        public new static Result<TValue> Failure(Error error) => new Result<TValue>(default, false, error);

        public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<Error, TResult> onFailure)
        {
            return IsSuccess
                ? onSuccess(Value!)
                : onFailure(Error!);
        }

        public static implicit operator Result<TValue>(TValue value) => Success(value);
    }
}