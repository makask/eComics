namespace eComics.Integrations.OpenMeteo
{
    public class OpenMeteoClient : IWeatherClient, IDisposable
    {
        private readonly HttpClient _httpClient;

        public OpenMeteoClient() 
        { 
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<decimal?> GetCurrentTemperature()
        {
            var url = "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current=temperature_2m,wind_speed_10m,relative_humidity_2m&houry=temperature_2m,relative_humidity_2m,wind_speed_10m";
            var result = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(url);
            return result.Current.Temperature_2m;
        }
    }
}
