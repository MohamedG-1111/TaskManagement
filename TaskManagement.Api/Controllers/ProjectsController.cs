using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagement.Api.Common.Responses;
using TaskManagement.Application.Features.Projects.Queries.GetAllProjects.Filters;
using TaskManagement.Application.Features.Projects.Queries.GetAllProjects.Responses;
using TaskManagement.Application.Features.Projects.Queries.GetProjectById;
using TaskManagement.Application.Features.Projects.Queries.GtAllProjects;
using TaskManagement.Domain.Common.Results;

namespace TaskManagement.Api.Controllers
{

    public class ProjectsController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public ProjectsController(IMediator mediator)
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

        [HttpGet]
        [SwaggerOperation(
       Summary = "Get all projects",
       Description = "Retrieves a paginated list of all projects."
   )]
        [SwaggerResponse(
       StatusCodes.Status200OK,
       "Projects retrieved successfully",
       typeof(ApiResponse<List<GetAllProjectsResponse>>))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllProjects(
       [FromQuery] PaginationParameters parameters,
       [FromQuery] ProjectFilter filter)
        {
            var result = await mediator.Send(new GetAllProjectsQuery(parameters, filter));
            return HandleResult(result);
        }
    }
}
