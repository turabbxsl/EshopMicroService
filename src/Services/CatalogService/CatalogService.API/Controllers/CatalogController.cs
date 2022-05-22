using CatalogService.API.Core.Application;
using CatalogService.API.Core.Domain;
using CatalogService.API.Infrastructure;
using CatalogService.API.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly CatalogContext _catalogContext;
        private readonly CatalogSettings _settings;


        public CatalogController(CatalogContext context, IOptionsSnapshot<CatalogSettings> settings)
        {
            _catalogContext = context ?? throw new ArgumentNullException(nameof(context));
            _settings = settings.Value;

            context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }


        //Get api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<CatalogItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ItemsAsync([FromQuery] int pageSize = 7, [FromQuery] int pageIndex = 0, string ids = null)
        {

            if (!string.IsNullOrEmpty(ids))
            {
                var items = await GetItemsByIDsAsync(ids);

                if (!items.Any())
                {
                    return BadRequest("IDs value invalid.Must be comma-seperated list of numbers");
                }

                return Ok(items);
            }

            var totalItems = await _catalogContext.CatalogItems.LongCountAsync();

            var itemsOnPage = await _catalogContext.CatalogItems
                                                   .OrderBy(x => x.Name)
                                                   .Skip(pageSize * pageIndex)
                                                   .Take(pageSize)
                                                   .ToListAsync();

            itemsOnPage = ChangeUrlPlaceholder(itemsOnPage);

            var model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
         }



        private async Task<List<CatalogItem>> GetItemsByIDsAsync(string ids)
        {
            var numsId = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numsId.All(x => x.Ok))
            {
                return new List<CatalogItem>();
            }

            var idsToSelect = numsId
                .Select(id => id.Value);

            var items = await _catalogContext.CatalogItems.Where(ci => idsToSelect.Contains(ci.Id)).ToListAsync();

            items = ChangeUrlPlaceholder(items);

            return items;
        }








        [HttpGet]
        [Route("items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CatalogItem>> ItemByIDAsync(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _catalogContext.CatalogItems.SingleOrDefaultAsync(x => x.Id == id);

            var baseUrl = _settings.PicBaseUrl;

            if (item != null)
            {
                item.PictureUrl = baseUrl + item.PictureFileName;

                return item;
            }

            return NotFound();
        }











        //GET api/v1/[controller]/items/withname/samplename[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/withname/{name:minlength(1)}")]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<CatalogItem>>> ItemsWithNameAsync(string name, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _catalogContext.CatalogItems
                                                  .Where(x => x.Name.StartsWith(name))
                                                  .LongCountAsync();

            var itemsOnPage = await _catalogContext.CatalogItems
                                                   .Where(x => x.Name.StartsWith(name))
                                                   .Skip(pageSize * pageIndex)
                                                   .Take(pageSize)
                                                   .ToListAsync();

            itemsOnPage = ChangeUrlPlaceholder(itemsOnPage);

            return new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
        }









        //GET api/v1/[controller]/items/type/1/brand[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("items/type/{catalogTypeID}/brand/{catalogBrandID:int?}")]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<CatalogItem>>> ItemsByTypeIDAndBrandIDAsync(int catalogTypeID, int? catalogBrandID, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<CatalogItem>)_catalogContext.CatalogItems;

            root = root.Where(x => x.CatalogTypeId == catalogTypeID);

            if (catalogBrandID.HasValue)
            {
                root = root.Where(x => x.CatalogBrandId == catalogBrandID);
            }

            var totalItems = await root.LongCountAsync();

            var itemsOnPage = await root
                                       .Skip(pageSize * pageIndex)
                                       .Take(pageSize)
                                       .ToListAsync();

            itemsOnPage = ChangeUrlPlaceholder(itemsOnPage);

            return new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
        }









        //GET api/v1/[controller]/items/type/all/brand[?pageSize=3&pageIndex=10]]
        [HttpGet]
        [Route("items/type/all/brand/{catalogBrandID:int?}")]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<CatalogItem>>> ItemsByBrandIDAsync(int? catalogBrandID, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {


            var root = (IQueryable<CatalogItem>)_catalogContext.CatalogItems;

            if (catalogBrandID.HasValue)
            {
                root = root.Where(x => x.CatalogBrandId == catalogBrandID);
            }

            var totalItems = await root.LongCountAsync();

            var itemsOnPage = await root
                                       .Skip(pageSize * pageIndex)
                                       .Take(pageSize)
                                       .ToListAsync();

            itemsOnPage = ChangeUrlPlaceholder(itemsOnPage);

            return new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);
        }









        //GET api/v1/[controller]/CatalogTypes
        [HttpGet]
        [Route("catalogTypes")]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<CatalogItem>>> CatalogTypesAsync()
        {
            return await _catalogContext.CatalogItems.ToListAsync();
        }









        //GET api/v1/[controller]/CatalogBrands
        [HttpGet]
        [Route("catalogBrands")]
        [ProducesResponseType(typeof(CatalogBrand), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<CatalogBrand>>> CatalogBrandsAsync()
        {
            return await _catalogContext.CatalogBrands.ToListAsync();
        }









        //PUT api/v1/[controller/items]
        [HttpPut]
        [Route("items")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateProductAsync([FromBody] CatalogItem productToUpdate)
        {
            var catalogItem = await _catalogContext.CatalogItems.SingleOrDefaultAsync(x => x.Id == productToUpdate.Id);

            if (catalogItem == null)
            {
                return NotFound(new { message = $"item with id {productToUpdate.Id} not found" });
            }


            var oldPrice = catalogItem.Price;
            var raiseProductPriceChangedEvent = oldPrice != productToUpdate.Price;


            //update current product
            catalogItem = productToUpdate;
            _catalogContext.CatalogItems.Update(productToUpdate);
            await _catalogContext.SaveChangesAsync();


            return CreatedAtAction(nameof(ItemByIDAsync), new { id = productToUpdate.Id }, null);
        }








        //POST api/v1/[controller/items]
        [HttpPost]
        [Route("items")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateProductAsync([FromBody] CatalogItem product)
        {

            var item = new CatalogItem()
            {
                CatalogBrandId = product.CatalogBrandId,
                CatalogTypeId = product.CatalogTypeId,
                Description = product.Description,
                Name = product.Name,
                PictureFileName = product.PictureFileName,
                Price = product.Price
            };

            _catalogContext.CatalogItems.Add(item);
            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(ItemByIDAsync), new { id = item.Id });
        }









        //POST api/v1/[controller/items]
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            var product = await _catalogContext.CatalogItems.SingleOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _catalogContext.CatalogItems.Remove(product);

            await _catalogContext.SaveChangesAsync();

            return NoContent();
        }








        private List<CatalogItem> ChangeUrlPlaceholder(List<CatalogItem> items)
        {
            var baseUrl = _settings.PicBaseUrl;

            foreach (var item in items)
            {
                if (item != null)
                {
                    item.PictureUrl = baseUrl + item.PictureFileName;
                }
            }

            return items;
        }


    }
}
