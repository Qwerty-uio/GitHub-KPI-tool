using System.Text.Json.Serialization;

namespace GitHub_KPI_tool_Application.Models.PullRequest;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum State
{
    Open,
    Closed,
    Merged
}