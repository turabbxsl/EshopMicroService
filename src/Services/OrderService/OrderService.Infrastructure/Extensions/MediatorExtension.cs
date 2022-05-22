using MediatR;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Extensions
{
    public static class MediatorExtension
    {


        public static async Task DispatchDomainEventsAsync(this IMediator mediator, OrderDBContext context)
        {

            // Deyisikliye ugramis DomainEvent-leri dolu olan BaseEntity ver dedik

            var domainEntities = context.ChangeTracker
                                       .Entries<BaseEntity>()
                                       .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            // DomainEntities-in icerisindeki butun DomainEventleri SelectMany ile secib Liste ceviririk
            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents)
                           .ToList();

            domainEntities.ToList()
                        .ForEach(entity => entity.Entity.ClearDomainEvents());


            /* 
             * Mediator Publish ile gonderende ayri,Send ile gonderende ayri isleyir
             * Send ile gonderende RequestHandler isleyir
             * Publish ile gonderende burdaki domaintEvent-i dinleyen ne qeder INotification-dan toremis class varsa onlar isleyecek
             * 
             * Meselen:OrderStartedDomainEvent  elave olunmus ise asagidaki Publish metodunu cagiranda Mediator avtomatik olaraq OrderStaredDomainEventHandler-deki Handle metodunu CAGIRACAQ
            */


            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }

        }


    }
}
