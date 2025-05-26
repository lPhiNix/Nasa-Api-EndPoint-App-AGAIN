using Nasa_Api_EndPoint_App_AGAIN.Dto;
using Newtonsoft.Json;

namespace Nasa_Api_EndPoint_App_AGAIN.Client;

/// <summary>
/// Concrete implementation of <see cref="INasaApiClient"/> that uses <see cref="HttpClient"/>
/// to communicate with NASA's Near Earth Object (NEO) REST API.
/// </summary>
/// <remarks>
/// The HttpClient instance is injected via constructor, following the Dependency Injection pattern.
/// This avoids socket exhaustion and improves testability and resource management.
/// </remarks>
public class NasaApiClient(HttpClient httpClient) : INasaApiClient
{
    // NASA API demo key. In production, this should be securely loaded from configuration or secrets manager.
    private const string ApiKey = "DEMO_KEY";

    // Base URL for the NASA Neo Feed API endpoint
    private const string BaseUrl = "https://api.nasa.gov/neo/rest/v1/feed";

    /// <summary>
    /// Fetches NEO data from NASA API for the given date range.
    /// </summary>
    /// <param name="start">Start date for the feed query (formatted as yyyy-MM-dd).</param>
    /// <param name="end">End date for the feed query (formatted as yyyy-MM-dd).</param>
    /// <returns>A <see cref="NasaApiResponse"/> object containing deserialized asteroid data.</returns>
    /// <exception cref="Exception">Thrown when the HTTP response indicates a failure.</exception>
    public async Task<NasaApiResponse?> GetNeoFeedAsync(DateTime start, DateTime end)
    {
        // Constructs the query string with start, end dates and API key
        var url = $"{BaseUrl}?start_date={start:yyyy-MM-dd}&end_date={end:yyyy-MM-dd}&api_key={ApiKey}";

        // Makes the HTTP GET request to NASA API
        var response = await httpClient.GetAsync(url);

        // Basic error handling for unsuccessful responses.
        // This could be expanded into custom exception types for better granularity.
        if (!response.IsSuccessStatusCode)
            throw new Exception("Error to consult NASA API");

        // Reads and deserializes the JSON response into a strongly-typed DTO
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<NasaApiResponse>(json);
    }
}
