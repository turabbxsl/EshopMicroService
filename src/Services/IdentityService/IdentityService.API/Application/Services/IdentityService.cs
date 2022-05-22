using IdentityService.API.Application.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Application.Services
{
    public class IdentityService : IIdentityService
    {


        public Task<LoginResponseModel> Login(LoginRequestModel requestModel)
        {

            //BurdaDatabase prosesleri ola biler

            var claims = new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier,requestModel.Username),
                new Claim(ClaimTypes.Name,"Turab Baxisli") //   <--- sehifede adini gostermek ucundur
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("turabbaxislininacari"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(claims: claims, expires: expiry, signingCredentials: creds, notBefore: DateTime.Now);

            var encodedJWT = new JwtSecurityTokenHandler().WriteToken(token);

            LoginResponseModel responseModel = new LoginResponseModel()
            {
                Username = requestModel.Username,
                UserToken = encodedJWT
            };

            return Task.FromResult(responseModel);
        }
    }
}
