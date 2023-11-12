using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestProject.Business.Admin;
using TestProject.Extensions;
using TestProject.Models.RequestModels;
using AuthorizeAttribute = TestProject.Extensions.AuthorizeAttribute;
using TestProject.Common;

namespace TestProject.Controllers
{
    [Route(Constants.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    [Authorize(Common.Enums.Role.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromQuery] EmployeeRequestModel input)
        {
            await _adminService.Register(input);
            return Ok();
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> ChargeEmployee(long userId, [FromBody]TransactionRequestModel input)
        {
            await _adminService.ChargeMemberBalance(userId, input);
            return Ok();
        }
        [HttpPost("{userId}")]
        public async Task<IActionResult> UpdateMemberBalance(long userId, [FromBody]TransactionRequestModel input)
        {
            await _adminService.UpdateMemberBalance(userId, input);
            return Ok();
        }
        [HttpPut("{userId}")]
        public async Task<IActionResult> RemoveInactiveMember(long userId)
        {
            await _adminService.RemoveInactiveMember(userId);
            return Ok();
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeteleInactiveMember(long userId)
        {
            await _adminService.DeleteInactiveMember(userId);
            return Ok();
        }
    }
}
