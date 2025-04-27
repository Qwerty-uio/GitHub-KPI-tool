using GitHub_KPI_tool_Application.Abstraction.Repositories;
using GitHub_KPI_tool_Application.Features.User.InsertUser;
using MediatR;

namespace GitHub_KPI_tool_Application.Features.Repository.InsertRepository;

public class CreateRepositoryQueryHandler: IRequestHandler<CreateRepositoryQuery, CreateRepositoryQueryResult>
{
    private readonly IRepositoryRepository _repositoryRepository;

    public CreateRepositoryQueryHandler(IRepositoryRepository repositoryRepository)
    {
        _repositoryRepository = repositoryRepository;
    }

    public async Task<CreateRepositoryQueryResult> Handle(CreateRepositoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repositoryRepository.InsertAsync(new Entities.RepositoryEntity()
            {
                Name = request.Name,
                Description = request.Description
            });

            return new CreateRepositoryQueryResult()
            {
                Id=result.Id
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}