namespace eComics.Services
{
    public interface IWeatherService
    {
        Task<decimal?> GetCurrentTemperature();
    }
}
