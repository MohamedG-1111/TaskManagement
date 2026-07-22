using FluentValidation;
using MediatR;
using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }


    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();


        var context = new ValidationContext<TRequest>(request);


        var failures = (await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken))))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();


        if (failures.Count > 0)
        {
            var errors = failures
                .Select(f =>
                    Error.Validation(
                        f.PropertyName,
                        f.ErrorMessage))
                .ToList();

            return GenerateFailureResult(errors);
        }


        return await next();
    }


    private static TResponse GenerateFailureResult(
        List<Error> errors)
    {
        var responseType = typeof(TResponse);

        if (responseType == typeof(Result))
        {
            return (TResponse)(object)
                Result.Failure(errors);
        }
        var valueType = responseType.GenericTypeArguments[0];


        var failResult = typeof(Result<>)
            .MakeGenericType(valueType)
            .GetMethod(
                nameof(Result<object>.Failure),
                new[] { typeof(IEnumerable<Error>) })
            !
            .Invoke(
                null,
                new object[] { errors });


        return (TResponse)failResult!;
    }
}