using TestProject.Models.ResponseModel;

namespace TestProject.Business.UserFolder
{
    public interface IUserService
    {
        Task<BalanceResponseModel> BalanceCheck(long userId);
    }
}
