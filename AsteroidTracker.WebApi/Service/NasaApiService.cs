using AsteroidTracker.ViewModel;
using AsteroidTracker.WebApi.Model;
using AsteroidTracker.WebApi.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace AsteroidTracker.WebApi.Service
{
    public class NasaApiService
    {


        private readonly Configuration _config;
        private readonly HttpClient _client = new();

        public NasaApiService(IOptions<Configuration> config)
        {
            _config = config.Value;
            _client.BaseAddress = new Uri(_config.NasaBaseUrl);
        }

        public NasaApiResponse GetByFilter(NasaApiRequest rq)
        {
            NasaApiResponse rs = new();
            Task<List<Asteroid>> t = Task.Run(() => GetAsync(rq));
            t.Wait();
            if (t != null)
            {
                List<Asteroid> asteroidList = t.Result;
                Dictionary<int, List<Asteroid>> paginatedAsteroids = new();
                int totalPages = Math.Abs(asteroidList.Count / rq.RegistersPerPage);
                for (int i = 0; i < totalPages; i++)
                {
                    paginatedAsteroids.Add(i, asteroidList.Skip(i * rq.RegistersPerPage).Take(rq.RegistersPerPage).ToList());
                }
                rs.Pages = paginatedAsteroids;
                rs.Success = true;
            }
            return rs;
        }

        public async Task<List<Asteroid>> GetAsync(NasaApiRequest rq )
        {
            List<Asteroid> pages = new();
            var response = await _client.GetAsync(CreateRequestUrl(rq));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject(result) as JObject;
                foreach (var day in jsonData["near_earth_objects"].Children())
                {
                    var item = day.Children().Children();
                    foreach (var x in item)
                    {
                        Planet planet = (Planet)Enum.Parse(typeof(Planet), x["close_approach_data"][0]["orbiting_body"].ToString());
                        bool isDangerous = Convert.ToBoolean(x["is_potentially_hazardous_asteroid"]);
                        if (isDangerous && planet == rq.PlanetName)
                        {
                            pages.Add(new Asteroid()
                            {
                                Diameter = CalcDiameter(x),
                                ApproachDate = Convert.ToDateTime(x["close_approach_data"][0]["close_approach_date"]),
                                ClosePlanet = planet,
                                Name = x["name"].ToString(),
                                Speed = Convert.ToDouble(x["close_approach_data"][0]["relative_velocity"]["kilometers_per_hour"])
                            });
                        }
                    }
                }
                return pages;
            }
            return null;
        }

        private string CreateRequestUrl(NasaApiRequest rq)
        {
            return $"{_config.NasaBaseUrl}start_date={rq.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}&end_date={rq.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}&api_key={_config.NasaApiKey}";
        }

        private double CalcDiameter(JToken x)
        {
            double min = Convert.ToDouble(x["estimated_diameter"]["kilometers"]["estimated_diameter_min"]);
            double max = Convert.ToDouble(x["estimated_diameter"]["kilometers"]["estimated_diameter_max"]);
            return (max + min) / 2;
        }

    }
}
