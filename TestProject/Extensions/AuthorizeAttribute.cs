using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;
using static TestProject.Common.Enums;

namespace TestProject.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Role _permissions;

        public AuthorizeAttribute(Role role)
        {
            _permissions = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var permissions = (List<Claim>)context.HttpContext.Items["Permissions"]!;
                if (!permissions.Any(p => _permissions.ToString() == p.Value.ToString()))
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }

            }
            catch (Exception)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
