using JwtTest2.Extensions;
using JwtTest2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTest2.Controllers
{
    [ApiController]
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        private IJwtService _jwtService;
        public ValuesController(IJwtService JwtService)
        {
            _jwtService = JwtService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Values()
        {
            var UserID = _jwtService.GetUser(User);

            try
            {
                return Ok(UserID);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
