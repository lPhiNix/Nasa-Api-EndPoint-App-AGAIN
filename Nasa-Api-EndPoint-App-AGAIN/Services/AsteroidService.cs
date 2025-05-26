using Nasa_Api_EndPoint_App_AGAIN.Dto;
using Nasa_Api_EndPoint_App_AGAIN.Services;


public class AsteroidService : IAsteroidService
{
    public Task<IEnumerable<AsteroidDto>> GetTopHazardousAsteroidsAsync(int days)
    {
        throw new NotImplementedException();
    }
}