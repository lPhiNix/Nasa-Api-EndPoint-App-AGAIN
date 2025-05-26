using Nasa_Api_EndPoint_App_AGAIN.Dto;

namespace Nasa_Api_EndPoint_App_AGAIN.Client;

/// <summary>
/// Defines the contract for communication with NASA's Near Earth Object API.
/// </summary>
/// <remarks>
/// This abstraction allows the HTTP communication logic to be easily mocked, 
/// replaced, or extended without affecting the consumers of this API client (e.g., services).
/// It promotes the Dependency Inversion Principle (DIP) as part of a clean architecture.
/// </remarks>
public interface INasaApiClient
{
    /// <summary>
    /// Retrieves Near Earth Object data from NASA between two dates.
    /// </summary>
    /// <param name="start">Start date for the query (inclusive).</param>
    /// <param name="end">End date for the query (inclusive).</param>
    /// <returns>
    /// A deserialized <see cref="NasaApiResponse"/> object or null if the response is empty.
    /// </returns>
    Task<NasaApiResponse?> GetNeoFeedAsync(DateTime start, DateTime end);
}