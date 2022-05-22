using BasketService.API.Core.Application.Repository;
using BasketService.API.Core.Application.Services;
using BasketService.API.Core.Domain.Models;
using BasketService.API.IntegrationEvents.Events;
using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BasketService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SebetController : ControllerBase
    {

        private readonly ISebetRepository _repository;
        private readonly IIdentityService _identityService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<SebetController> _logger;

        public SebetController(ISebetRepository repository, IIdentityService identityService, IEventBus eventBus, ILogger<SebetController> logger)
        {
            _repository = repository;
            _identityService = identityService;
            _eventBus = eventBus;
            _logger = logger;
        }




        // Uygulamanin isleyib islemediyini bildirir
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Basket Service is Up and Running");
        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Sebet), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Sebet>> GetSebetByIdAsync(string id)
        {
            var sebet = await _repository.GetSebetAsync(id);

            return sebet ?? new Sebet(id);
        }


        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(Sebet), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Sebet>> UpdateSebetAsync([FromBody] Sebet sebet)
        {
            return Ok(await _repository.UpdateSebetAsync(sebet));
        }




        [HttpPost]
        [Route("additem")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddItemToSebet([FromBody] SebetItem sebetItem)
        {
            var userid = _identityService.GetUserName().ToString();

            var sebet = await _repository.GetSebetAsync(userid);

            if (sebet == null)
            {
                sebet = new Sebet(userid);
            }

            sebet.SebetItems.Add(sebetItem);

            await _repository.UpdateSebetAsync(sebet);

            return Ok();
        }





        [HttpPost]
        [Route("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Checkout([FromBody] SebetCheckout checkout  /* [FromHeader(Name="x-request-id")] string requestid*/)
        {

            var userid = checkout.Buyer;

            var sebet = await _repository.GetSebetAsync(userid);

            if (sebet == null)
            {
                return BadRequest();
            }

            var username = _identityService.GetUserName(); // Bu yuxaridaki checkout.Buyer ile eynidi


            //OrderService deyirik ki,bir Order yarana biler.Sen get bu Order melumatlari ile bidene Sifaris yarat deye bilmek ucun OrderCreatedIntegrationEvent-i yaradirig

            var eventMessage = new OrderCreatedIntegrationEvent(userid, username, checkout.City, checkout.Street, checkout.State, checkout.Country, checkout.Zipcode, checkout.CardNumber, checkout.CartHolderName, checkout.CartExpiration, checkout.CartSecurityNumber, checkout.CartTypeId, checkout.Buyer, sebet);

            try
            {
                _eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error publishing IntegrationEvent : {eventMessage.Id} from BasketService.Api");

                throw;
            }

            return Accepted();
        }





        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteSebetByIdAsync(string id)
        {
            var sebet = _repository.GetSebetAsync(id);

            if (sebet == null)
            {
                _logger.LogError("Sebet Delete is error");
            }

            await _repository.DeleteSebetAsync(id);
        }



    }
}
