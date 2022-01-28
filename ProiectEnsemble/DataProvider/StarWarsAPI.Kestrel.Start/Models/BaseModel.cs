using System.Text.Json.Serialization;

namespace StarWarsAPI.Server.Models;

public abstract class BaseModel
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("edited")]
    public DateTime Edited { get; set; }
}