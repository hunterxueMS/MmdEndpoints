using System.Text.Json;
using System.Text.Json.Serialization;

namespace HunMmdEndpoints.Models
{
    public class EndpointModelResp
    {
        [JsonPropertyName("openapi")]
        public string? Openapi { get; set; }
        [JsonPropertyName("info")]
        public JsonElement? Info { get; set; }
        [JsonPropertyName("servers")]
        public JsonElement? Servers { get; set; } 
        [JsonPropertyName("paths")]
        public JsonElement? RawPaths { get; set; }
        [JsonPropertyName("components")]
        public JsonElement? Components { get; set; }
        [JsonPropertyName("security")]
        public JsonElement? Security { get; set; }
        [JsonIgnore]
        public List<EndpointModelItem> Paths { get; set; } = new();
    }
}
public class EndpointModelItem
{
    public string Path { get; }
    public string OperationId { get; }

    public EndpointModelItem(string path, string operationId, string method)
    {
        Path = path;
        OperationId = operationId;
        Method = method;
    }

    public string? Summary { get; set; }
    public string Method { get; set; }
    public List<Parameter> Parameters { get; set; } = new();
}

public class Parameter
{
    [JsonInclude]
    public string Name;
    [JsonInclude]
    public string? Desc;

    public Parameter(string name)
    {
        Name = name;
    }
}

