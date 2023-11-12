using TestProject.Models.Entities;
using TestProject.Models.RequestModels;
using TestProject.Models.ResponseModel;

namespace TestProject.Business.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseModel> Login(LoginRequestModel input);

    }
}
