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
        protected IActionResult HandleResult(
            Result result, int successStatusCode = StatusCodes.Status200OK, string? message = null)
        {
            if (result.IsFailure)
            {
                return ApiProblemDetailsFactory.Failure(
                    result,
                    HttpContext.TraceIdentifier,
                    HttpContext.Request.Path);
            }

            return StatusCode(
                successStatusCode,
                ApiResponseFactory.Success(
                    data: null,
                    traceId: HttpContext.TraceIdentifier,
                    message: message));
        }

        protected IActionResult HandleResult<T>(Result<T> result, int successStatusCode = StatusCodes.Status200OK, string? message = null)
        {
            if (result.IsFailure)
            {
                return ApiProblemDetailsFactory.Failure(
                    result,
                    HttpContext.TraceIdentifier,
                    HttpContext.Request.Path);
            }

            return StatusCode(
                successStatusCode,
                ApiResponseFactory.Success(
                    data: result.Value,
                    traceId: HttpContext.TraceIdentifier,
                    message: message));
        }
    }
}