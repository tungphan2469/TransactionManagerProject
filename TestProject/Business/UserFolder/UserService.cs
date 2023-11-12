using Microsoft.EntityFrameworkCore;
using TestProject.Models;
using TestProject.Models.ResponseModel;
using static TestProject.Common.Enums;
using System.Security.Claims;
using TestProject.Common;

namespace TestProject.Business.UserFolder
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BalanceResponseModel> BalanceCheck(long userId)
        {
            var user = await _dbContext.Employees.Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync();
            if (user == null) { throw new Exception(string.Format(Constants.ExceptionMessage.NOT_FOUND, "User")); }

            var res = new BalanceResponseModel()
            {
                UserName = user.UserName,
                Balance = PersonalBalanceCheck(userId)
            };
            return res;
        }
        public long PersonalBalanceCheck(long userId)
        {
            var personalHistory = _dbContext.TransactionHistory
                                .Where(x => x.EmployeeId == userId)
                                .ToList()
                                .OrderBy(x => x.Id);
            long currentBalance = 0;
            foreach (var transaction in personalHistory)
            {
                if (transaction.TransactionType == Transaction.Charge)
                {
                    currentBalance += transaction.TransactionAmount;
                };
                if (transaction.TransactionType == Transaction.Purchase)
                {
                    currentBalance -= transaction.TransactionAmount;
                };
            }
            return currentBalance;
        }
    }
}
