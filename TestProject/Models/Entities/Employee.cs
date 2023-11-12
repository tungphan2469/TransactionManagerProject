using System.Security.Cryptography;
using TestProject.Models.Base;
using static TestProject.Common.Enums;
namespace TestProject.Models.Entities
{
    public class Employee : BaseEntitySoftDelete
    {
        public string FullName { get; set; }
        public Role Role { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual ICollection<TransactionHistory> DepositHistories { get; set; }

    }
}
