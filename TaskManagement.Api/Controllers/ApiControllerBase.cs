using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Common.Responses.Factories;
using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult HandleResult(Result result) =>
    result.IsFailure
        ? ApiProblemDetailsFactory.Failure(result, HttpContext.TraceIdentifier, HttpContext.Request.Path)
        : Ok(ApiResponseFactory.Success(data: (object?)null, traceId: HttpContext.TraceIdentifier));

        protected IActionResult HandleResult<T>(Result<T> result, string? message = null) =>
            result.IsFailure
                ? ApiProblemDetailsFactory.Failure(result, HttpContext.TraceIdentifier, HttpContext.Request.Path)
                : Ok(ApiResponseFactory.Success(data: result.Value, traceId: HttpContext.TraceIdentifier, message: message));
    }
}
