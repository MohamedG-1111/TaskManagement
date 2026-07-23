using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Common.Responses.Errors;
using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Api.Common.Responses.Factories
{
    public static class ApiProblemDetailsFactory
    {
        private static ProblemDetails Create(Result result, string traceId, string requestPath)
        {
            var error = result.Errors.FirstOrDefault();
            (int status, string title) = ErrorTypeMapper.Resolve(error!.Type);
            var problem = new ProblemDetails()
            {
                Type = $"https://api.taskmanagement.com/errors/{error.Code}",
                Status = status,
                Title = title,
                Detail = error.Message,
                Instance = requestPath

            };
            problem.Extensions["traceId"] = traceId;
            if (error.Type == ErrorType.Validation)
            {
                var errors = result.Errors
                    .GroupBy(x => x.Code)
                    .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.Message).ToArray());
                problem.Extensions["Error"] = errors;

            }
            return problem;
        }

        public static ObjectResult Failure(
        Result result,
        string traceId,
        string requestPath)
        {
            var problem = Create(result, traceId, requestPath);

            return new ObjectResult(problem)
            {
                StatusCode = problem.Status
            };
        }
    }
}
