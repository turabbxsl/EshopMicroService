using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Domain.Models.Users
{
    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }


        public UserLoginRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
