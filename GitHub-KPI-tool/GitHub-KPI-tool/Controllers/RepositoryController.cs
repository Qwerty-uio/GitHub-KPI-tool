using GitHub_KPI_tool_Application.Entities;
using GitHub_KPI_tool_Application.Features.GitHub.GetRepository;
using GitHub_KPI_tool_Application.Features.Repository.InsertRepository;
using GitHub_KPI_tool.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitHub_KPI_tool.Controllers;

[Controller]
[Route("/repository/")]
public class RepositoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RepositoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRepositoryEntityRequest repository)
    {
        var query = new CreateRepositoryQuery(repository.Owner,repository.Name, repository.Description);
        var result = await _mediator.Send(query);

        return Created();
    }
}