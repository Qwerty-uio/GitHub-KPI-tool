using GitHub_KPI_tool_Application.Abstraction.ApiClients;
using MediatR;

namespace GitHub_KPI_tool_Application.Features.GitHub.GetRepository;

public class GetRepositoryQueryHandler: IRequestHandler<GetRepositoryQuery, GetRepositoryQueryResult>
{
    private readonly IGetRepository _repository;

    public GetRepositoryQueryHandler(IGetRepository repository)
    {
        _repository = repository;
    }


    public async Task<GetRepositoryQueryResult> Handle(GetRepositoryQuery request, CancellationToken cancellationToken)
    {
        var repository = await _repository.Get(request.Owner,request.Repository);
        var result = new GetRepositoryQueryResult()
        {
            Repository = repository
        };
        return result;
    }
}