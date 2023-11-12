
namespace TestProject.Models.RequestModels
{
    public class TransactionRequestModel
    {
        public long TransactionAmount { get; set; }
        public string TransactionNote { get; set; } = string.Empty;
    }
}
