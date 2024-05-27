namespace eComics.Integrations.YrNoClient
{
    public class YrNoClient : IWeatherClient, IDisposable
    {
        private readonly string _jsonData;

        public YrNoClient() 
        {
            _jsonData = File.ReadAllText("yrno-data.txt");
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task<decimal?> GetCurrentTemperature()
        {
            throw new NotImplementedException();
        }
    }
}
