using Nasa_Api_EndPoint_App_AGAIN.Dto;

namespace Nasa_Api_EndPoint_App_AGAIN.Services;

/// <summary>
/// Interface for the asteroid processing service.
/// </summary>
/// <remarks>
/// This abstraction is used to decouple the controller and other components
/// from the concrete implementation. It supports testability, extensibility,
/// and adheres to the Dependency Inversion Principle.
/// </remarks>
public interface IAsteroidService
{
    /// <summary>
    /// Retrieves the top N most dangerous asteroids from NASA's data.
    /// </summary>
    /// <param name="days">How many days ahead to include in the search (max: 7).</param>
    /// <returns>A list of hazardous asteroid DTOs ordered by size.</returns>
    Task<IEnumerable<AsteroidDto>?> GetTopHazardousAsteroidsAsync(int days);
}