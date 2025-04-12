using GitHub_KPI_tool_Application.Features.UserRepositoryActivity.InsertUserRepositoryActivity;
using GitHub_KPI_tool.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitHub_KPI_tool.Controllers;

[ApiController]
[Route("/user-repository-activity")]
public class UserRepositoryActivityController:ControllerBase
{
    private readonly IMediator _mediator;

    public UserRepositoryActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Handle([FromBody]CreateUserRepositoryActivityEntityRequest request)
    {
        var query = new CreateUserRepositoryActivityQuery(request.UserId,request.RepositoryId);
        var result = await _mediator.Send(query);

        return Created();
    }
}