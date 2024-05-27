namespace eComics.Integrations
{
    public interface IWeatherClient
    {
        Task<decimal?> GetCurrentTemperature();
    }
}
