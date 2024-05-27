namespace eComics.Integrations.OpenMeteo
{
    public class OpenMeteoResponse
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public OpenMeteoCurrentWeather Current { get; set; }
    }
}
