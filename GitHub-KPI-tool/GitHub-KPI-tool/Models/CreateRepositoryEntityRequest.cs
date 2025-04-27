namespace GitHub_KPI_tool.Models;

public class CreateRepositoryEntityRequest
{
    public string Owner { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}