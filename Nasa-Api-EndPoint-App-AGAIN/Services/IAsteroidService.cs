using Nasa_Api_EndPoint_App_AGAIN.Dto;

namespace Nasa_Api_EndPoint_App_AGAIN.Services;

public interface IAsteroidService
{
    Task<IEnumerable<AsteroidDto>> GetTopHazardousAsteroidsAsync(int days);
}