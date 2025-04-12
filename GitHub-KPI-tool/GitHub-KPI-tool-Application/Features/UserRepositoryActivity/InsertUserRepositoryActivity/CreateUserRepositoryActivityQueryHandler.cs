using GitHub_KPI_tool_Application.Abstraction.Repositories;
using GitHub_KPI_tool_Application.Entities;
using MediatR;

namespace GitHub_KPI_tool_Application.Features.UserRepositoryActivity.InsertUserRepositoryActivity;

public class CreateUserRepositoryActivityQueryHandler:IRequestHandler<CreateUserRepositoryActivityQuery,CreateUserRepositoryActivityQueryResult>
{
    private readonly IUserRepositoryActivityRepository _userRepositoryActivityRepository;

    public CreateUserRepositoryActivityQueryHandler(IUserRepositoryActivityRepository userRepositoryActivityRepository)
    {
        _userRepositoryActivityRepository = userRepositoryActivityRepository;
    }


    public async Task<CreateUserRepositoryActivityQueryResult> Handle(CreateUserRepositoryActivityQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userRepositoryActivityRepository.InsertAsync(new UserRepositoryActivityEntity()
            {
                UserId = request.UserId,
                RepositoryId = request.RepositoryId
            });

            return new CreateUserRepositoryActivityQueryResult()
            {
                Id = result.Id
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}