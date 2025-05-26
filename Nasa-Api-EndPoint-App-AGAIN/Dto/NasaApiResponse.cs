using Newtonsoft.Json;

namespace Nasa_Api_EndPoint_App_AGAIN.Dto;

/// <summary>
/// Root model representing the NASA API response for near-earth objects.
/// </summary>
/// <remarks>
/// This model directly mirrors the JSON schema returned by the NASA NEO Feed API.
/// It's used internally to deserialize the raw response into structured objects
/// that can then be processed and transformed into application-friendly DTOs.
/// </remarks>
public class NasaApiResponse
{
    /// <summary>
    /// A dictionary where each key is a date (yyyy-MM-dd) and the value is a list of asteroids for that day.
    /// </summary>
    [JsonProperty("near_earth_objects")]
    public Dictionary<string, List<Asteroid>> NearEarthObjects { get; set; } = new();
}

/// <summary>
/// Represents a single asteroid object from the NASA API.
/// </summary>
public class Asteroid
{
    /// <summary>
    /// The name of the asteroid.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = "";

    /// <summary>
    /// Indicates whether the asteroid is potentially hazardous.
    /// </summary>
    [JsonProperty("is_potentially_hazardous_asteroid")]
    public bool IsPotentiallyHazardousAsteroid { get; set; }

    /// <summary>
    /// Diameter information grouped by unit of measurement.
    /// </summary>
    [JsonProperty("estimated_diameter")]
    public EstimatedDiameter EstimatedDiameter { get; set; } = new();

    /// <summary>
    /// A list of dates and data when the asteroid is predicted to closely approach Earth or another body.
    /// </summary>
    [JsonProperty("close_approach_data")]
    public List<CloseApproachData> CloseApproachData { get; set; } = new();
}

/// <summary>
/// Container for estimated diameter in multiple measurement systems.
/// </summary>
public class EstimatedDiameter
{
    /// <summary>
    /// Diameter values expressed in kilometers.
    /// </summary>
    [JsonProperty("kilometers")]
    public DiameterKilometers Kilometers { get; set; } = new();
}

/// <summary>
/// Detailed diameter range in kilometers.
/// </summary>
public class DiameterKilometers
{
    /// <summary>
    /// The minimum estimated diameter in kilometers.
    /// </summary>
    [JsonProperty("estimated_diameter_min")]
    public double EstimatedDiameterMin { get; set; }

    /// <summary>
    /// The maximum estimated diameter in kilometers.
    /// </summary>
    [JsonProperty("estimated_diameter_max")]
    public double EstimatedDiameterMax { get; set; }
}

/// <summary>
/// Contains information about a single close approach event of an asteroid.
/// </summary>
public class CloseApproachData
{
    /// <summary>
    /// The date of the close approach in string format.
    /// </summary>
    [JsonProperty("close_approach_date")]
    public string? CloseApproachDate { get; set; } = "";

    /// <summary>
    /// Relative velocity of the asteroid during its close approach.
    /// </summary>
    [JsonProperty("relative_velocity")]
    public RelativeVelocity RelativeVelocity { get; set; } = new();

    /// <summary>
    /// The celestial body the asteroid is approaching (e.g., Earth).
    /// </summary>
    [JsonProperty("orbiting_body")]
    public string OrbitingBody { get; set; } = "";
}

/// <summary>
/// Velocity data of the asteroid.
/// </summary>
public class RelativeVelocity
{
    /// <summary>
    /// The speed of the asteroid in kilometers per hour.
    /// </summary>
    [JsonProperty("kilometers_per_hour")]
    public string KilometersPerHour { get; set; } = "";
}