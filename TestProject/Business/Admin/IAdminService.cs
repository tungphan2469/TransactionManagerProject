using TestProject.Models.Entities;
using TestProject.Models.RequestModels;
using TestProject.Models.ResponseModel;

namespace TestProject.Business.Admin
{
    public interface IAdminService
    {
        Task<Employee> Register(EmployeeRequestModel input);
        Task ChargeMemberBalance(long userId, TransactionRequestModel input);
        Task UpdateMemberBalance(long userId, TransactionRequestModel input);
        Task RemoveInactiveMember(long userId);
        Task DeleteInactiveMember(long userId);
    }
}
