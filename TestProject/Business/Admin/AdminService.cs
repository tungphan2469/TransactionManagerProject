using Microsoft.EntityFrameworkCore;
using TestProject.Models;
using TestProject.Models.Entities;
using TestProject.Models.RequestModels;
using TestProject.Models.ResponseModel;
using static TestProject.Common.Enums;
using TestProject.Common;

namespace TestProject.Business.Admin
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _dbContext;
        public AdminService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> Register(EmployeeRequestModel input)
        {
            PasswordEncript.CreatePasswordHash(input.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var userDb = _dbContext.Employees.Where(x => x.UserName == input.UserName).FirstOrDefault();
            if (userDb != null) { throw new Exception(string.Format(Constants.ExceptionMessage.INVALID, "User name")); }
            Employee newUser = new Employee
            {
                FullName = input.FullName,
                Role = input.Role,
                UserName = input.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _dbContext.Add(newUser);
            _dbContext.SaveChanges();
            return newUser;
        }
        public async Task ChargeMemberBalance(long userId, TransactionRequestModel input)
        {
            try
            {
                var memberDb = await _dbContext.Employees.Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync();
                if (memberDb == null) { throw new Exception(string.Format(Constants.ExceptionMessage.NOT_FOUND, nameof(userId))); }
                TransactionHistory newTransaction = new TransactionHistory()
                {
                    EmployeeId = userId,
                    TransactionType = Transaction.Charge,
                    TransactionAmount = input.TransactionAmount,
                    TransactionNote = input.TransactionNote
                };

                _dbContext.Add(newTransaction);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateMemberBalance(long userId, TransactionRequestModel input)
        {
            try
            {
                var memberDb = await _dbContext.Employees.Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync();
                if (memberDb == null) { throw new Exception(string.Format(Constants.ExceptionMessage.NOT_FOUND, nameof(userId))); }
                var currentBalance = PersonalBalanceCheck(userId);
                if (currentBalance < input.TransactionAmount)
                {
                    throw new Exception(string.Format(Constants.ExceptionMessage.INSUFFICIENT_BALANCE));
                }
                AddNewTransaction(userId, input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task RemoveInactiveMember(long userId)
        {
            try
            {
                var userDb = await _dbContext.Employees.Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync();
                if (userDb == null) { throw new Exception(string.Format(Constants.ExceptionMessage.NOT_FOUND, nameof(userId))); }
                userDb.isDeleted = true;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task DeleteInactiveMember(long userId)
        {
            try
            {
                var userDb = await _dbContext.Employees.Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync();
                if (userDb == null) { throw new Exception(string.Format(Constants.ExceptionMessage.NOT_FOUND, nameof(userId))); }
                _dbContext.Employees.Remove(userDb);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #region PRIVATE FUNCTIONS
        private long PersonalBalanceCheck(long userId)
        {
            var personalHistory = _dbContext.TransactionHistory
                                .Where(x => x.EmployeeId == userId)
                                .ToList()
                                .OrderByDescending(x => x.EmployeeId);
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

        private void AddNewTransaction(long userId, TransactionRequestModel input)
        {
            try
            {
                TransactionHistory newTransaction = new TransactionHistory();
                newTransaction.TransactionType = Transaction.Purchase;
                newTransaction.TransactionAmount = input.TransactionAmount;
                newTransaction.EmployeeId = userId;
                newTransaction.TransactionNote = input.TransactionNote;
                _dbContext.Add(newTransaction);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        #endregion

    }
}
