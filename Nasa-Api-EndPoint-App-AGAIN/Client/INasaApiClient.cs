using Nasa_Api_EndPoint_App_AGAIN.Dto;

namespace Nasa_Api_EndPoint_App_AGAIN.Client;

public interface INasaApiClient
{
    Task<NasaApiResponse?> GetNeoFeedAsync(DateTime start, DateTime end);
}