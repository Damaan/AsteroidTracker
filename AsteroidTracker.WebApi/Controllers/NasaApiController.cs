using AsteroidTracker.ViewModel;
using AsteroidTracker.WebApi.Service;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

namespace AsteroidTracker.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NasaApiController : ControllerBase
    {
        private readonly ILogger<NasaApiController> _logger;
        private readonly NasaApiService _ApiService;


        public NasaApiController(ILogger<NasaApiController> logger, NasaApiService ApiService)
        {
            _logger = logger;
            _ApiService = ApiService;
        }
        [AuthorizeRequest]
        [HttpPost(Name = "Asteroids")]
        public IActionResult Get(NasaApiRequest rq)
        {

            NasaApiResponse rs = _ApiService.GetByFilter(rq);
            if (rs.Success)
                return Ok(rs);
            else
                return BadRequest();

        }
    }
}