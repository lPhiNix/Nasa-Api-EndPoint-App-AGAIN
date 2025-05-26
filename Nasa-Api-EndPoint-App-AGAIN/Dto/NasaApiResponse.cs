using Newtonsoft.Json;

namespace Nasa_Api_EndPoint_App_AGAIN.Dto;

public class NasaApiResponse
{
    [JsonProperty("near_earth_objects")]
    public Dictionary<string, List<Asteroid>> NearEarthObjects { get; set; } = new();
}

public class Asteroid
{
    [JsonProperty("name")]
    public string Name { get; set; } = "";

    [JsonProperty("is_potentially_hazardous_asteroid")]
    public bool IsPotentiallyHazardousAsteroid { get; set; }

    [JsonProperty("estimated_diameter")]
    public EstimatedDiameter EstimatedDiameter { get; set; } = new();

    [JsonProperty("close_approach_data")]
    public List<CloseApproachData> CloseApproachData { get; set; } = new();
}

public class EstimatedDiameter
{
    [JsonProperty("kilometers")]
    public DiameterKilometers Kilometers { get; set; } = new();
}

public class DiameterKilometers
{
    [JsonProperty("estimated_diameter_min")]
    public double EstimatedDiameterMin { get; set; }

    [JsonProperty("estimated_diameter_max")]
    public double EstimatedDiameterMax { get; set; }
}

public class CloseApproachData
{
    [JsonProperty("close_approach_date")]
    public string? CloseApproachDate { get; set; } = "";

    [JsonProperty("relative_velocity")]
    public RelativeVelocity RelativeVelocity { get; set; } = new();

    [JsonProperty("orbiting_body")]
    public string OrbitingBody { get; set; } = "";
}

public class RelativeVelocity
{
    [JsonProperty("kilometers_per_hour")]
    public string KilometersPerHour { get; set; } = "";
}