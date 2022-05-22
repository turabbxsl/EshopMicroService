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
    public class PaymentMethodEntityConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {

            builder.ToTable("paymentmethods", OrderDBContext.DEFAULT_SCHEMA);

            builder.Ignore(x => x.DomainEvents);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

            builder.Property<int>("BuyerId")
                .IsRequired();

            // CardHolderName ---> bu menim ucun field
            builder.Property(x => x.CardHolderName)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CardHolderName")
                .HasMaxLength(200)
                .IsRequired();

            // Alias ---> bu da menim ucun field
            builder.Property(x => x.Alias)
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .HasColumnName("Alias")
               .HasMaxLength(200)
               .IsRequired();

            // CardNumber ---> bu da menim ucun field
            builder.Property(x => x.CardNumber)
              .UsePropertyAccessMode(PropertyAccessMode.Field)
              .HasColumnName("CardNumber")
              .HasMaxLength(200)
              .IsRequired();

            // Expiration ---> bu da menim ucun field
            builder.Property(x => x.Expiration)
              .UsePropertyAccessMode(PropertyAccessMode.Field)
              .HasColumnName("Expiration")
              .HasMaxLength(25)
              .IsRequired();

            // CardTypeId ---> bu da menim ucun field
            builder.Property(x => x.CardTypeId)
              .UsePropertyAccessMode(PropertyAccessMode.Field)
              .HasColumnName("CardTypeId")
              .IsRequired();

            builder.HasOne(x => x.CardType)
                .WithMany()
                .HasForeignKey(x => x.CardTypeId);

        }
    }
}
