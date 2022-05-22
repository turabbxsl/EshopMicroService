#pragma checksum "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Pages\Catalog\CatalogPage.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8c9dd636ea31b6c08a7d438f2f046ebc7750f7f4"
// <auto-generated/>
#pragma warning disable 1591
namespace WebApp.Pages.Catalog
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
    [Microsoft.AspNetCore.Components.RouteAttribute("/catalog")]
    public partial class CatalogPage : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "row");
#nullable restore
#line 7 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Pages\Catalog\CatalogPage.razor"
     if (model.Data != null && model.Data.Any())
    {
        foreach (var catalogItem in model.Data)
        {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<WebApp.Components.Catalog.ProductView.ProductView>(2);
            __builder.AddAttribute(3, "CatalogItem", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<WebApp.Domain.Models.CatalogModels.CatalogItem>(
#nullable restore
#line 11 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Pages\Catalog\CatalogPage.razor"
                                                                            catalogItem

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(4, "Onclick", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 11 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Pages\Catalog\CatalogPage.razor"
                                                                                                  ()=>AddToCart(catalogItem)

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
#nullable restore
#line 12 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Pages\Catalog\CatalogPage.razor"
        }
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 19 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Pages\Catalog\CatalogPage.razor"
       

    PaginatedItemsViewModel<CatalogItem> model = new PaginatedItemsViewModel<CatalogItem>();


    [Inject]
    ICatalogService CatalogService { get; set; }


    [Inject]
    IIdentityService IdentityService { get; set; }

    [Inject]
    ISebetService SebetService { get; set; }

    [Inject]
    NavigationManager NavigationManager { get; set; }


    [Inject]
    AppStateManager AppState { get; set; }




    protected async override Task OnInitializedAsync()
    {

        // sehife acilanda OnInitialized metodu isleyecek ve CatalogServisden metodu cagiraraq modelin icini dolduracag
        model = await CatalogService.GetCatalogItems();
    }



    public async Task AddToCart(CatalogItem catalogItem)
    {
        if (!IdentityService.isLoggedIn)
        {
            // Istifadecini Login-e ReturnUrl ile birlikde gonderirik
            NavigationManager.NavigateTo($"login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}");
            return;
        }

        await SebetService.AddItemToSebet(catalogItem.Id);

        AppState.UpdateCart(this);
    }


#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591