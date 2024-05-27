namespace eComics.Integrations.OpenMeteo
{
    public class OpenMeteoCurrentWeather
    {
        public DateTime Time { get; set; }
        public int Interval { get; set; }
        public decimal Temperature_2m { get; set; }
        public decimal Wind_speed_10m { get; set; }
        public int Relative_humidity_2m { get; set; }

    }
}
