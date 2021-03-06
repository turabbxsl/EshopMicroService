// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace WebApp.Pages.SebetPages
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/sebet")]
    public partial class Sebet : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 77 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Pages\SebetPages\Sebet.razor"
       


    WebApp.Domain.Models.ViewModels.Sebet sebetModel = new WebApp.Domain.Models.ViewModels.Sebet();


    [Inject]
    ISebetService sebetService { get; set; }

    [Inject]
    NavigationManager NavigationManager { get; set; }


    protected async override Task OnInitializedAsync()
    {
        sebetModel = await sebetService.GetSebet();
    }


    void Checkout()
    {
        NavigationManager.NavigateTo("/create-order");
        Console.WriteLine("Navigation Create-Order");
    }


    async Task RemoveItemFromList(SebetItem sebetItem)
    {
        sebetModel.SebetItems.Remove(sebetItem);

        sebetModel = await sebetService.UpdateSebet(sebetModel);
    }


#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
