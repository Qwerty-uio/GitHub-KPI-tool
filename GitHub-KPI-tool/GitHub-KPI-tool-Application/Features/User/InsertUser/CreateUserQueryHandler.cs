using GitHub_KPI_tool_Application.Abstraction.Repositories;
using MediatR;

namespace GitHub_KPI_tool_Application.Features.User.InsertUser;

public class CreateUserQueryHandler: IRequestHandler<CreateUserQuery, CreateUserQueryResult>
{
    private readonly IUserRepository _userRepository;

    public CreateUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<CreateUserQueryResult> Handle(CreateUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userRepository.InsertAsync(new Entities.UserEntity()
            {
                UserName = request.UserName,
                Email = request.Email
            });
            
            return new CreateUserQueryResult()
            {
                Id = result.Id,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}