using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.Business.Auth;
using TestProject.Extensions;
using TestProject.Models.RequestModels;
using TestProject.Common;

namespace TestProject.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost]
        public async Task<IActionResult>Login([FromBody] LoginRequestModel input)
        {
            var res = await _authService.Login(input);
            return Ok(res);
        }

    }
}
