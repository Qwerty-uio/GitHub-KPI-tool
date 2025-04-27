using GitHub_KPI_tool_Application.Features.GitHub.GetRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitHub_KPI_tool.Controllers;

[Route("/github-kpi/repository")]
[ApiController]
public class GitHubRepositoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GitHubRepositoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("/github-kpi/repository")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(string owner, string repository)
    {
        var result = await _mediator.Send(new GetRepositoryQuery(owner, repository));
        
        return Ok(result);
    }
}