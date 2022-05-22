using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BasketService.API.Core.Application.Services
{
    public class IdentityService : IIdentityService
    {

        private readonly IHttpContextAccessor httpContext;

        public IdentityService(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public string GetUserName()
        {
            return httpContext.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
