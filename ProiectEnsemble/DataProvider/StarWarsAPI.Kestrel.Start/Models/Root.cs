using System.Text.Json.Serialization;

namespace StarWarsAPI.Server.Models;

public class Root
{
    [JsonPropertyName("films")]
    public string Films { get; set; } = string.Empty;

    [JsonPropertyName("people")]
    public string People { get; set; } = string.Empty;

    [JsonPropertyName("planets")]
    public string Planets { get; set; } = string.Empty;

    [JsonPropertyName("species")]
    public string Species { get; set; } = string.Empty;

    [JsonPropertyName("starships")]
    public string Starships { get; set; } = string.Empty;

    [JsonPropertyName("vehicles")]
    public string Vehicles { get; set; } = string.Empty;
}