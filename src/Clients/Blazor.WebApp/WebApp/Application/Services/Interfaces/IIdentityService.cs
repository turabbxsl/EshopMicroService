using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Application.Services.Interfaces
{
    public interface IIdentityService
    {

        string GetUsername();
        string GetUserToken();
        bool isLoggedIn { get; }
        Task<bool> Login(string username, string password);
        void Logout();

    }
}
