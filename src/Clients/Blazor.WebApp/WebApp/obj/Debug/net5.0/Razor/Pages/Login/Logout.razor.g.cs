#pragma checksum "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Pages\Login\Logout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "51f473d0c594c48c78776ec4088640a6aed69c22"
// <auto-generated/>
#pragma warning disable 1591
namespace WebApp.Pages.Login
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using WebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using WebApp.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Application.Services.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Domain.Models.Users;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Domain.Models.CatalogModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Domain.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using Application.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using WebApp.Domain.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\_Imports.razor"
using WebApp.Infrastructure;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Pages\Login\Logout.razor"
using System.Web;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/logout")]
    public partial class Logout : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h3>Logout</h3>");
        }
        #pragma warning restore 1998
#nullable restore
#line 9 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Pages\Login\Logout.razor"
       


    [Inject]
    IIdentityService identityService { get; set; }

    [Inject]
    NavigationManager navigationManager { get; set; }

    [Inject]
    AppStateManager appState { get; set; }



    protected override void OnInitialized()
    {
        identityService.Logout();

        var collection = HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);

        string returnUrl = collection.Get("returnUrl") ?? "/";

        appState.LoginChanged(this);

        navigationManager.NavigateTo(returnUrl);
    }




#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
