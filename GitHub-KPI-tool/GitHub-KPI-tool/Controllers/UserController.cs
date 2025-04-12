using GitHub_KPI_tool_Application.Entities;
using GitHub_KPI_tool_Application.Features.User.InsertUser;
using GitHub_KPI_tool_Infrastructure.Contexts;
using GitHub_KPI_tool.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitHub_KPI_tool.Controllers;

[Route("users/")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserEntityRequest user)
    {
        var query = new CreateUserQuery(user.UserName, user.Email);
        
        var result = await _mediator.Send(query);
        
        return Created();
    }
}