
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class SubProductConfiguration : IEntityTypeConfiguration<SubProduct>
{
    public void Configure(EntityTypeBuilder<SubProduct> builder)
    {

        builder.ToTable("SubProduct");
        builder.HasIndex(x => x.ProductId);

        builder.Property(x => x.Unit).HasMaxLength(450); 
        builder.Property(x => x.Color).HasMaxLength(450);
        builder.Property(x => x.ProductId);
        builder.Property(x => x.Stock)
                    .HasDefaultValue(0);

        builder.Property(sp => sp.Price)
                    .HasColumnType("decimal(18,2)");

        builder.Property(sp => sp.RetailPrice)
                    .HasColumnType("decimal(18,2)");

        builder.HasOne(d => d.Product).WithMany(p => p.SubProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Ignore(e => e.DomainEvents);
    }
}


