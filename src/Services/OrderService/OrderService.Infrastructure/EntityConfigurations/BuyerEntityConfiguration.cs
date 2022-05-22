using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModels.BuyerAgregate;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.EntityConfigurations
{
    public class BuyerEntityConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.ToTable("buyers", OrderDBContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.DomainEvents);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(100);

            builder.HasMany(x => x.PaymentMethods)
                   .WithOne()
                   .HasForeignKey(x => x.Id)
                   .OnDelete(DeleteBehavior.Cascade);


            //EntityFramework PaymentMethods a ulasirken bunun bir field olaraq dusunulmesini edirik
            var navigation = builder.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
