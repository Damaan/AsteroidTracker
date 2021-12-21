using AsteroidTracker.ViewModel;

namespace AsteroidTracker.WebApi.Model
{
    public class Asteroid
    {
        public string Name { get; set; }
        public double Diameter { get; set; }
        public double Speed { get; set; }
        public DateTime ApproachDate { get; set; }
        public Planet ClosePlanet { get; set; }
    }
}
