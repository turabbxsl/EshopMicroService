using CatalogService.API.Core.Domain;
using CatalogService.API.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.API.Infrastructure.EntityConfiguration
{
    public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog", CatalogContext.DEFAULT_SCHEMA);

            builder.Property(x => x.Id)
                .UseHiLo("catalog_hilo")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(x => x.Price)
                .IsRequired(true);

            builder.Property(x => x.AvailableStock)
                .HasMaxLength(500);

            builder.Property(x => x.PictureFileName)
                .IsRequired(false);

            builder.Ignore(x => x.PictureUrl);

            builder.HasOne(x => x.CatalogBrand)
                .WithMany()
                .HasForeignKey(x => x.CatalogBrandId);

            builder.HasOne(x => x.CatalogType)
                .WithMany()
                .HasForeignKey(x => x.CatalogTypeId);
        }
    }


}
