using IdentityService.API.Application.Models;
using IdentityService.API.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IIdentityService identityService;

        public AuthController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }



        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel requestModel)
        {
            var result = await identityService.Login(requestModel);

            return Ok(result);
        }







    }
}
