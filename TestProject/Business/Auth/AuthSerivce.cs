using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TestProject.Models;
using TestProject.Models.Entities;
using TestProject.Models.RequestModels;
using TestProject.Models.ResponseModel;
using static TestProject.Common.Enums;
using TestProject.Common;

namespace TestProject.Business.Auth
{
    public class AuthSerivce : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _config;

        public AuthSerivce(AppDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel input)
        {
            var res = new LoginResponseModel();
            var user = await _dbContext.Employees.Where(x => x.UserName == input.UserName).FirstOrDefaultAsync();
            if (user == null)
            {
                res.LoginStatus = string.Format(Constants.ExceptionMessage.NOT_FOUND, "User");
                return res;
            }

            if (!PasswordEncript.VerifyPasswordHash(input.Password, user.PasswordHash, user.PasswordSalt))
            {
                res.LoginStatus = string.Format(Constants.ExceptionMessage.WRONG_PASSWORD);
                return res;
            }
            string token = CreateToken(user);

            res.LoginStatus = string.Format(Constants.Message.LOGIN_SUCCESS);
            res.Token = token;
            return res;
        }

        #region PRIVATE FUNCTIONS
        
        private string CreateToken(Employee user)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Id", user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("Appsettings:AuthSecret").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        } 
        #endregion
    }
}
