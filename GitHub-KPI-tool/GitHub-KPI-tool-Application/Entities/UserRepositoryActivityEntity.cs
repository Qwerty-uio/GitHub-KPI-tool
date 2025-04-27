namespace GitHub_KPI_tool_Application.Entities;

public class UserRepositoryActivityEntity : BaseEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RepositoryId { get; set; }
    // check all activities you can get
}