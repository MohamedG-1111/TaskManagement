using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagement.Api.Common.Responses;
using TaskManagement.Application.Features.Projects.Queries.GetProjectById;
using TaskManagement.Application.Features.Projects.Queries.Responses;

namespace TaskManagement.Api.Controllers
{

    public class ProjectController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public ProjectController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        [SwaggerOperation(
     Summary = "Get project by id",
     Description = "Retrieves a project using its unique identifier."
 )]
        [SwaggerResponse(StatusCodes.Status200OK, "Project retrieved successfully", typeof(ApiResponse<GetProjectByIdResponse>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Project was not found", typeof(ProblemDetails))]
        public async Task<IActionResult> GetProjectById([FromRoute] Guid id)
        {
            var result = await mediator.Send(new GetProjectByIdQuery(id));
            return HandleResult(result);
        }
    }
}
