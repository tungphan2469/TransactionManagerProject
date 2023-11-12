using System.ComponentModel.DataAnnotations.Schema;
using TestProject.Models.Base;
using static TestProject.Common.Enums;

namespace TestProject.Models.Entities
{
    public class TransactionHistory : BaseEntity
    {
        public long TransactionAmount { get; set; }
        public long EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }
        public Transaction TransactionType { get; set; }
        public string TransactionNote { get; set; } = string.Empty;
        public DateTime TransactionTime { get; set; } = DateTime.Now;
    }
}
