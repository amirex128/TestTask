using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Models;
using infrastructure.EntityFramework.Context;
using Microsoft.IdentityModel.Tokens;

namespace Application.Auth
{
    public class AuthApplication
    {
        private SqliteContext _sqliteContext;

        public AuthApplication(SqliteContext sqliteContext)
        {
            _sqliteContext = sqliteContext;
        }


        public bool RegisterUser(RegisterViewModel model)
        {
            var userAlreadyExists = _sqliteContext.Users.SingleOrDefault(x =>
                x.Username == model.Username && x.Password == model.Password);

            if (userAlreadyExists != null)
            {
                var user = new User(model.FirstName, model.LastName, model.Username, model.Password);
                _sqliteContext.Users.Add(user);
                _sqliteContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public string GetToken(string username)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TestTask"));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(claims: new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }
                , expires: DateTime.Now.AddMonths(1), signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public bool CheckAuth(LoginViewModel model)
        {
            var user = _sqliteContext.Users.SingleOrDefault(x =>
                x.Username == model.Username && x.Password == model.Password);
            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}