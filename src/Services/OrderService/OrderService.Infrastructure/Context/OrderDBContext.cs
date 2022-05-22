using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.AggregateModels.BuyerAgregate;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.EntityConfigurations;
using OrderService.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Context
{
    public class OrderDBContext : DbContext, IUnitOfWork
    {

        public const string DEFAULT_SCHEMA = "ordering";
        private readonly IMediator mediator;

        public OrderDBContext() : base()
        {

        }



        public OrderDBContext(DbContextOptions<OrderDBContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<CartType> CartTypes { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }



        //DomainEventleri Handle edib onlari MediatR pattern ile cagira bilmek ucun
        //SaveEntitiesAsync metodu cagirildigi zaman entity icerisinde DomainEvents-lerin icerisinde neler varsa onlari firlatacayig ondan sonra datalar Database-e SaveChanges deyilerek Save olunur
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {

            await mediator.DispatchDomainEventsAsync(this);

            //Bu entitye aid butun datalar yarandi,sen artiq get bu datalari Database-e yaz
            await base.SaveChangesAsync(cancellationToken);


            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CartTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentMethodEntityConfiguration());

        }

    }
}
