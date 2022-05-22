using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ApiGateway.Models.Sebet;
using Web.ApiGateway.Services.Interfaces;

namespace Web.ApiGateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {

        private readonly ICatalogService catalogService;
        private readonly ISebetService sebetService;

        public BasketController(ICatalogService catalogService, ISebetService sebetService)
        {
            this.catalogService = catalogService;
            this.sebetService = sebetService;
        }

        [HttpPost]
        [Route("items")]
        public async Task<ActionResult> AddSebetItemAsync([FromBody] AddSebetItemRequest request)
        {

            if (request is null || request.Quantity == 0)
            {
                return BadRequest("Invalid Payload");
            }

            // CatalogItem-i aliriq
            var item = await catalogService.GetCatalogItemAsync(request.CatalogItemId);

            // Sebeti aliriq
            var currentSebet = await sebetService.GetById(request.SebetId);

            var product = currentSebet.SebetItems.SingleOrDefault(i => i.ProductId == item.Id);

            // Eger Product Sebetde var ise 
            if (product != null)
            {
                product.Quantity += request.Quantity;
            }
            else
            {
                // Eger Product Sebetde yoxdursa

                currentSebet.SebetItems.Add(new SebetItemDTO()
                {
                    UnitPrice = item.Price,
                    PictureUrl = item.PictureUrl,
                    ProductId = item.Id,
                    Quantity = request.Quantity,
                    Id = Guid.NewGuid().ToString(),
                    ProductName = item.Name
                });

            }

            await sebetService.UpdateAsync(currentSebet);

            return Ok();
        }





    }
}
