
using TestProject.Common;
using TestProject.Models;
using TestProject.Models.Entities;
using static TestProject.Common.Enums;
namespace TestProject.Models;

public partial class DataSeeding
{
    public static void DevelopementSeed(AppDbContext dbContext, IConfiguration config)
    {


        if (!dbContext.Employees.Any())
        {
            dbContext.Employees.AddRange(GenerateEmployee());
            dbContext.SaveChanges();
        }
    }

    private static List<Employee> GenerateEmployee()
    {
        var password = "123456";
        PasswordEncript.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        return new List<Employee>
        {
            new Employee
            {
            FullName = "Tran Viet Tung",
            Role = Role.Admin,
            UserName = "Admin",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
            },
            new Employee
            {
            FullName = "Tran Viet Tung",
            Role = Role.Member,
            UserName = "Member",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
            }
        };
    }
}
