﻿using System.Text.Json.Serialization;

namespace StarWarsAPI.Server.Models;

public class Planets : BaseModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("rotation_period")]
    public string RotationPeriod { get; set; } = string.Empty;

    [JsonPropertyName("orbital_period")]
    public string OrbitalPeriod { get; set; } = string.Empty;

    [JsonPropertyName("diameter")]
    public string Diameter { get; set; } = string.Empty;

    [JsonPropertyName("climate")]
    public string Climate { get; set; } = string.Empty;

    [JsonPropertyName("gravity")]
    public string Gravity { get; set; } = string.Empty;

    [JsonPropertyName("terrain")]
    public string Terrain { get; set; } = string.Empty;

    [JsonPropertyName("surface_water")]
    public string SurfaceWater { get; set; } = string.Empty;

    [JsonPropertyName("population")]
    public string Population { get; set; } = string.Empty;

    [JsonPropertyName("residents")]
    public List<string> Residents { get; set; } = new List<string>();

    [JsonPropertyName("films")]
    public List<string> Films { get; set; } = new List<string>();
}