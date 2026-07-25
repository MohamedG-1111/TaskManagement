using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManagement.Api.Common.Responses;
using TaskManagement.Application.Features.Projects.Commands.CreateProject;
using TaskManagement.Application.Features.Projects.Commands.DeleteProject;
using TaskManagement.Application.Features.Projects.Commands.UpdateProject;
using TaskManagement.Application.Features.Projects.Commands.UpdateProject.Requests;
using TaskManagement.Application.Features.Projects.Commands.UpdateProjectStatus;
using TaskManagement.Application.Features.Projects.Commands.UpdateProjectStatus.Requests;
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

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await mediator.Send(new DeleteProjectByIdCommand(id));

            return HandleResult(result, StatusCodes.Status204NoContent, "Project Deleted successfully");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateProject(CreateProjectCommand command)
        {
            var result = await mediator.Send(command);

            return HandleResult(result, StatusCodes.Status201Created, "Project created successfully");

        }


        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateProject([FromRoute] Guid id, [FromBody] UpdateProjectRequest request)
        {
            var command = new UpdateProjectCommand(
                id,
                request.Name,
                request.Description,
                request.StartDate,
                request.EndDate);
            var result = await mediator.Send(command);
            return HandleResult(result, message: "Project updated successfully");
        }

        [HttpPatch("{id:guid}/status")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
        [SwaggerOperation(
    Summary = "Update project status",
    Description = """
                  Updates the status of a project.
                  Allowed values:
                  - Active
                  - Completed
                  - Cancelled
                  - Archived
                  """)]
        public async Task<IActionResult> UpdateProjectStatus([FromRoute] Guid id, [FromBody] UpdateProjectStatusRequest request)
        {
            var command = new UpdateProjectStatusCommand(id, request.Status);

            var result = await mediator.Send(command);

            return HandleResult(result, message: "Project status updated successfully");
        }
    }
}
