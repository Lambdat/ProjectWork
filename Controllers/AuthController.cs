using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.DTOs;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        //Per effettuare la registrazione/accesso bisogna effettuare
        //2 chiamate HTTP di tipo POST:

        //1) Registrazione avente nella Route il percorso "signup"

        //2) Accesso avente nella Route il percorso "login"

        [HttpPost("signup")]
        public IActionResult Registration([FromBody] UserRegistrationRequest request)
        {
            if (_service.Register(request))
            {
                return Ok("Registration completed");
            }
            return BadRequest("User already exists");
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] UserLoginRequest userLogin)
        {
            try
            {
                return Ok(_service.Login(userLogin.Ssn,userLogin.Username, userLogin.Password));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
