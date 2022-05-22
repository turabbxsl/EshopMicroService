using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Domain.Models.Users
{
    public class UserLoginResponse
    {
        public string Username { get; set; }
        public string Token { get; set; }

    }
}
