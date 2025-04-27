using GitHub_KPI_tool_Application.Features.GitHub.GetPullRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitHub_KPI_tool.Controllers;

[Route("/github-kpi/pull-requests")]
[ApiController]
public class GitHubPullRequestsController: ControllerBase
{
    private readonly IMediator _mediator;

    public GitHubPullRequestsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Route("/github-kpi/pull-requests")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPullRequests(string owner, string repository)
    {
        var result = await _mediator.Send(new GetPullRequestQuery(owner, repository));
        
        return Ok(result);
    }
    
}