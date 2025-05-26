using Nasa_Api_EndPoint_App_AGAIN.Dto;
using Newtonsoft.Json;

namespace Nasa_Api_EndPoint_App_AGAIN.Client;

public class NasaApiClient(HttpClient httpClient) : INasaApiClient
{
    private const string ApiKey = "DEMO_KEY";
    private const string BaseUrl = "https://api.nasa.gov/neo/rest/v1/feed";

    public async Task<NasaApiResponse?> GetNeoFeedAsync(DateTime start, DateTime end)
    {
        var url = $"{BaseUrl}?start_date={start:yyyy-MM-dd}&end_date={end:yyyy-MM-dd}&api_key={ApiKey}";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error to consult NASA API");

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<NasaApiResponse>(json);
    }
}
