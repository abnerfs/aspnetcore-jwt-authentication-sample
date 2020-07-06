using JwtTest2.Extensions;
using JwtTest2.Models;
using JwtTest2.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTest2.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private IJwtService _jwtService;

        public AuthController(IJwtService JwtService)
        {
            _jwtService = JwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserAuth Auth)
        {
            try
            {
                if(Auth.Email == "teste@teste.com" && Auth.Password == "1234")
                {
                    var Token = _jwtService.GenerateToken(Auth.Email);
                    return Ok(Token);
                }

                return Forbid();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
