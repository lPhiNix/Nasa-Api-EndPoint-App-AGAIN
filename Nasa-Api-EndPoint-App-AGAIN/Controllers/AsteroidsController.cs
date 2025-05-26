using Microsoft.AspNetCore.Mvc;
using Nasa_Api_EndPoint_App_AGAIN.Services;

[ApiController]
[Route("[controller]")]
public class AsteroidsController(IAsteroidService asteroidService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsteroids([FromQuery] int? days)
    {
        switch (days)
        {
            case null:
                return BadRequest("The 'days' parameter is required.");
            case < 1:
            case > 7:
                return BadRequest("The 'days' parameter must be between 1 and 7.");
            default:
            {
                var result = await asteroidService.GetTopHazardousAsteroidsAsync(days.Value);
                return Ok(result);
            }
        }
    }
}