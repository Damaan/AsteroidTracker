namespace AsteroidTracker.ViewModel
{
    public class NasaApiRequest
    {
        public Planet PlanetName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrderField { get; set; }
        public bool OrderType { get; set; }
        public int RegistersPerPage { get; set; }
        public int PageNumber { get; set; }
    }
}
