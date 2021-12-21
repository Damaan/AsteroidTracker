using AsteroidTracker.WebApi.Model;

namespace AsteroidTracker.ViewModel
{
    public class NasaApiResponse
    {
        public bool Success { get; set; }
        public Dictionary<int, List<Asteroid>> Pages { get; set; }
    }
}
