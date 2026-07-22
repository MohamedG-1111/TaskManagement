namespace TaskManagement.Domain.Common.Results;

public class Result
{
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public IReadOnlyList<Error> Errors { get; }

    protected Result(bool isSuccess, IReadOnlyList<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        if (isSuccess && errors.Count > 0)
        {
            throw new InvalidOperationException(
                "A successful result cannot contain errors.");
        }

        if (!isSuccess && errors.Count == 0)
        {
            throw new InvalidOperationException(
                "A failed result must contain at least one error.");
        }

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result Success()
        => new(true, Array.Empty<Error>());

    public static Result Failure(Error error)
        => new(false, new[] { error });

    public static Result Failure(IEnumerable<Error> errors)
        => new(false, errors.ToArray());
}

public sealed class Result<TValue> : Result
{
    private readonly TValue? _value;

    public TValue Value =>
        IsSuccess
            ? _value!
            : throw new InvalidOperationException(
                "Cannot access the value of a failed result.");

    private Result(TValue value)
        : base(true, Array.Empty<Error>())
    {
        _value = value;
    }

    private Result(
        bool isSuccess,
        IReadOnlyList<Error> errors)
        : base(isSuccess, errors)
    {
        _value = default;
    }

    public static Result<TValue> Success(TValue value)
        => new(value);

    public new static Result<TValue> Failure(Error error)
        => new(false, new[] { error });

    public new static Result<TValue> Failure(IEnumerable<Error> errors)
        => new(false, errors.ToArray());

    public TResult Match<TResult>(
        Func<TValue, TResult> onSuccess,
        Func<IReadOnlyList<Error>, TResult> onFailure)
    {
        return IsSuccess
            ? onSuccess(Value)
            : onFailure(Errors);
    }

    public static implicit operator Result<TValue>(TValue value)
        => Success(value);
}