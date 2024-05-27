using eComics.Integrations;

namespace eComics.Services
{
    public class WeatherService: IWeatherService
    {
        private readonly IWeatherClient _weatherClient;
        public WeatherService(IWeatherClient weatherClient) 
        {
            _weatherClient = weatherClient;
        }
        public async Task<decimal?> GetCurrentTemperature()
        { 
            var temp = await _weatherClient.GetCurrentTemperature();
            return temp;
        }
    }
}
