using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.EntityConfigurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders", OrderDBContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Ignore(x => x.DomainEvents);

            // Address valueobject oldugu ucun yeni bir table yaratmiriq onun yerine Order table-in da ---> Order_Street,Order_City kimi sutunlar yaranir
            builder.OwnsOne(x => x.Address, a =>
               {
                   a.WithOwner();
               });

            // orderStatusId fieldine get, Databasede --->OrderStatusId sutunu yarat deyirik
            builder.Property<int>("orderStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderStatusId")
                .IsRequired();

            // OrderItems IReadonlyCollection oldugu icin set oluna bilmir ve geriye ancaq _orderItems qaytarir.

            //OrderItems propertisini tapiriq
            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));

            //Bu propertiye ulasicagimiz yol ise Field olacaq
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(x => x.Buyer)
                .WithMany()
                .HasForeignKey(y => y.BuyerId);

            builder.HasOne(x => x.OrderStatus)
                .WithMany()
                .HasForeignKey("orderStatusId");

        }
    }
}
