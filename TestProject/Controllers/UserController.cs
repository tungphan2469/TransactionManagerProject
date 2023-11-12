using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestProject.Business.UserFolder;
using TestProject.Extensions;
using TestProject.Common;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace TestProject.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentBalance()
        {
            //var userId = User?.Identity?.Name;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userId = identity.FindFirst("Id")?.Value;
                var userName = identity.FindFirst(ClaimTypes.Name)?.Value;

                _ = long.TryParse(userId, out long res);
                var balanceRes = await _userService.BalanceCheck(res);
                return Ok(balanceRes);
            }
            return BadRequest();

        }
    }
}
