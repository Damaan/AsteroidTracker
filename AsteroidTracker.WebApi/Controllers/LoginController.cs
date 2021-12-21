using AsteroidTracker.ViewModel;
using AsteroidTracker.WebApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace AsteroidTracker.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly LoginService _loginService;


        public LoginController(ILogger<LoginController> logger, LoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [HttpPost(Name = "Login")]
        public IActionResult Login(LoginRequest rq)
        {

            LoginResponse rs = _loginService.Authenticate(rq);
            if (rs.Success)
                return Ok(rs);
            else
                return BadRequest(rs);
            
        }
    }
}