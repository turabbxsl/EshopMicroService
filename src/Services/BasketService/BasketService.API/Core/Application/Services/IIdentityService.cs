using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.API.Core.Application.Services
{
    public interface IIdentityService
    {

        //Servisimize Token ile birlikde gonderilmis Claimlarin icerisindeki istifadeci adini (NameIdentifier) versin
        string GetUserName();
    }
}
