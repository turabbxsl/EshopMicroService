using IdentityService.API.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Application.Services
{
    public interface IIdentityService
    {

        Task<LoginResponseModel> Login(LoginRequestModel requestModel);

    }
}
