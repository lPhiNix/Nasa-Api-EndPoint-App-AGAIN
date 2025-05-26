using System.Globalization;
using Nasa_Api_EndPoint_App_AGAIN.Client;
using Nasa_Api_EndPoint_App_AGAIN.Dto;
using Nasa_Api_EndPoint_App_AGAIN.Services;
using Nasa_Api_EndPoint_App_AGAIN.Util;

/// <summary>
/// Service responsible for processing asteroid data retrieved from NASA's NEO API.
/// </summary>
/// <remarks>
/// This service encapsulates the core business logic of the application:
/// fetching the data from the external API, filtering hazardous asteroids,
/// transforming them into application-friendly DTOs, and selecting the top 3 by size.
/// 
/// This separation of concerns adheres to SOLID principles, specifically:
/// - Single Responsibility: only handles logic related to asteroid processing.
/// - Dependency Inversion: relies on an interface `INasaApiClient` for external API access,
///   making it easy to test or swap implementations.
/// </remarks>
public class AsteroidService(INasaApiClient nasaApiClient) : IAsteroidService
{
    /// <summary>
    /// Fetches and returns the top 3 largest potentially hazardous asteroids within the next N days.
    /// </summary>
    /// <param name="days">Number of future days to fetch data for (maximum 7).</param>
    /// <returns>
    /// A list of up to 3 <see cref="AsteroidDto"/> objects representing the most dangerous asteroids.
    /// Returns null if data retrieval fails or no hazardous asteroids are found.
    /// </returns>
    public async Task<IEnumerable<AsteroidDto>?> GetTopHazardousAsteroidsAsync(int days)
    {
        // Define the date range: from today to today + N days
        var startDate = DateTime.UtcNow.Date;
        var endDate = startDate.AddDays(days);

        // Fetch raw data from NASA API
        var data = await nasaApiClient.GetNeoFeedAsync(startDate, endDate);

        // Process and filter data
        var asteroids = data?.NearEarthObjects
            .SelectMany(x => x.Value) // Flatten the dictionary of lists
            .Where(a => a.IsPotentiallyHazardousAsteroid) // Only include hazardous ones
            .Select(a =>
            {
                var approach = a.CloseApproachData.FirstOrDefault();

                // Calculate the average diameter using a utility method
                var diameter = DiameterCalculator.GetAverageKm(a.EstimatedDiameter.Kilometers);

                return new AsteroidDto
                {
                    Name = a.Name,
                    Diameter = diameter,
                    Speed = double.Parse(approach?.RelativeVelocity.KilometersPerHour ?? "0", CultureInfo.InvariantCulture),
                    Date = approach?.CloseApproachDate,
                    Planet = approach?.OrbitingBody
                };
            })
            .OrderByDescending(a => a.Diameter) // Sort by size (most dangerous)
            .Take(3) // Limit to top 3
            .ToList();

        return asteroids;
    }
}
