using static TestProject.Common.Enums;
using TestProject.Models.Entities;

namespace TestProject.Models.RequestModels
{
    public class EmployeeRequestModel
    {
        public required string FullName { get; set; }
        public Role Role { get; set; }
        public string UserName { get; set; } = string.Empty;
        public required string Password { get; set; }
    }
}
