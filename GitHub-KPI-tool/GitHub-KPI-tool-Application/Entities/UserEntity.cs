namespace GitHub_KPI_tool_Application.Entities;

public class UserEntity : BaseEntity
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
}