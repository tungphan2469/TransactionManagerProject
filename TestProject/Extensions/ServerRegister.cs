using TestProject.Business.Admin;
using TestProject.Business.Auth;
using TestProject.Business.UserFolder;

namespace TestProject.Extensions
{
    public static class ServerRegister
    {
        public static void ServiceRegister(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IAuthService, AuthSerivce>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
