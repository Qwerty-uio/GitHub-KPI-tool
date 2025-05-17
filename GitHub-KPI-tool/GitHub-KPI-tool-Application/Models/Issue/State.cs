using System.Text.Json.Serialization;

namespace GitHub_KPI_tool_Application.Models.Issue;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum State
{
    Open,
    Closed
}