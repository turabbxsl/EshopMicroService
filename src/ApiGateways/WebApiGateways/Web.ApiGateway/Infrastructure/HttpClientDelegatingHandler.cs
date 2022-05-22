using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Web.ApiGateway.Infrastructure
{
    public class HttpClientDelegatingHandler : DelegatingHandler
    {

        private readonly IHttpContextAccessor httpContextAccessor;


        public HttpClientDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }



        // bu o zaman tetiklenecek ki, colden bize bir istek geldi ve biz icerde httpCLiet istifade etdik.HttpClient-da Send olmadan evvel(yəni Send dedikde Post,Get),qarsi terefe ulasacaq her hansi bir istek tetiklenmeden bura isleyecek
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            // APigateway-e gonderilen requestin icerisinde Authorization varmi yoxmu onu tapir
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"];


            // Eger varsa yəni bize BearerToken gonderibse
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                // Gelen requestin header-in da Authorization ile elaqeli herhansi birsey var mi
                if (request.Headers.Contains("Authorization"))
                {
                    // Authorization var ise belke kohnelmis ola biler deye onu silirik
                    request.Headers.Remove("Authorization");
                }

                // Sildikten sonra ona yenisini veririk
                request.Headers.Add("Authorization", new List<string>(authorizationHeader));
            }

            // Artiq biz HttpClienti istifade ederken bize gonderilen bir Authorization varsa onu da Set etmis olduq

            return base.SendAsync(request, cancellationToken);
        }





    }
}
