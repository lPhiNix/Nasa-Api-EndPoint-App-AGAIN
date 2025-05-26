using Microsoft.AspNetCore.Mvc;
using Nasa_Api_EndPoint_App_AGAIN.Services;

namespace Nasa_Api_EndPoint_App_AGAIN.Controllers;

/// <summary>
/// Controller that exposes an endpoint to retrieve the top 3 most hazardous asteroids 
/// from NASA's Near Earth Object API for a given number of days.
/// </summary>
/// <remarks>
/// This controller adheres to RESTful design principles and uses dependency injection
/// to delegate business logic to the IAsteroidService layer. This helps maintain
/// a clean separation of concerns and improves testability and maintainability.
/// </remarks>
[ApiController]
[Route("[controller]")]
public class AsteroidsController(IAsteroidService asteroidService) : ControllerBase
{
    /// <summary>
    /// Gets the top 3 hazardous asteroids within a given number of days from today.
    /// </summary>
    /// <param name="days">The number of days (1–7) for which to query asteroids. Must be between 1 and 7.</param>
    /// <returns>
    /// 200 OK with a list of hazardous asteroids if the input is valid;
    /// 400 Bad Request with an error message otherwise.
    /// </returns>
    /// <response code="200">Returns the top 3 hazardous asteroids</response>
    /// <response code="400">If the 'days' parameter is missing or outside the allowed range</response>
    [HttpGet]
    public async Task<IActionResult> GetAsteroids([FromQuery] int? days)
    {
        // Validates that the 'days' parameter is provided.
        switch (days)
        {
            case null:
                return BadRequest("The 'days' parameter is required.");
            case < 1:
            case > 7:
                // Validates that 'days' falls within the acceptable range (1 to 7).
                return BadRequest("The 'days' parameter must be between 1 and 7.");
            default:
            {
                // Delegates the logic to the service layer to handle business rules.
                var result = await asteroidService.GetTopHazardousAsteroidsAsync(days.Value);
                return Ok(result); // Returns the filtered list of asteroids as JSON.
            }
        }
    }
}