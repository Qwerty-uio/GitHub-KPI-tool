namespace GitHub_KPI_tool_Application.Entities;

public class RepositoryEntity : BaseEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}