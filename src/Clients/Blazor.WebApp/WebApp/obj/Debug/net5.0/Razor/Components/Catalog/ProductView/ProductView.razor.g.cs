#pragma checksum "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Components\Catalog\ProductView\ProductView.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "126fc97e21c2e060d38695ca31eb32953f085be6"
// <auto-generated/>
#pragma warning disable 1591
namespace WebApp.Components.Catalog.ProductView
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
    public partial class ProductView : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "col-md-3 col-sm-6");
            __builder.OpenElement(2, "div");
            __builder.AddAttribute(3, "class", "product-grid");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "product-image");
            __builder.OpenElement(6, "a");
            __builder.AddAttribute(7, "href", "#");
            __builder.AddAttribute(8, "class", "image");
            __builder.OpenElement(9, "img");
            __builder.AddAttribute(10, "class", "pic-1");
            __builder.AddAttribute(11, "src", 
#nullable restore
#line 10 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Components\Catalog\ProductView\ProductView.razor"
                                          "http://localhost:5004/" + CatalogItem.PictureUrl

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(12, "\r\n\r\n        ");
            __builder.OpenElement(13, "div");
            __builder.AddAttribute(14, "class", "product-content");
            __builder.OpenElement(15, "h3");
            __builder.AddAttribute(16, "class", "title");
            __builder.OpenElement(17, "a");
            __builder.AddAttribute(18, "href", "#");
            __builder.AddContent(19, 
#nullable restore
#line 17 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Components\Catalog\ProductView\ProductView.razor"
                             CatalogItem.Name

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(20, "\r\n            ");
            __builder.OpenElement(21, "div");
            __builder.AddAttribute(22, "class", "price");
            __builder.AddMarkupContent(23, "\r\n                $ ");
            __builder.AddContent(24, 
#nullable restore
#line 20 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Components\Catalog\ProductView\ProductView.razor"
                   CatalogItem.price.ToString("N2")

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\r\n\r\n            ");
            __builder.OpenElement(26, "a");
            __builder.AddAttribute(27, "class", "add-to-cart");
            __builder.AddAttribute(28, "href", "javascript:void(0)");
            __builder.AddAttribute(29, "onclick", 
#nullable restore
#line 23 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Components\Catalog\ProductView\ProductView.razor"
                                                                       Onclick

#line default
#line hidden
#nullable disable
            );
            __builder.AddMarkupContent(30, "\r\n                Add To Cart\r\n            ");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 34 "C:\Users\HP\source\repos\SellingMS\src\Clients\Blazor.WebApp\WebApp\Components\Catalog\ProductView\ProductView.razor"
       

    [Parameter]
    public CatalogItem CatalogItem { get; set; }



    [Parameter]
    public EventCallback<MouseEventArgs> Onclick { get; set; }






#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
