

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class PurchaseInvoiceConfiguration : IEntityTypeConfiguration<PurchaseInvoice>
{
    public void Configure(EntityTypeBuilder<PurchaseInvoice> builder)
    {
        builder.Property(x => x.InvoiceNumber).HasMaxLength(255); 
        builder.Property(x => x.SupplierId);
        builder.Property(x => x.InvoiceType).HasMaxLength(255); 
        builder.Property(x => x.PaymentStatus).HasMaxLength(255); 
        builder.Property(x => x.PaymentMethod).HasMaxLength(255); 
        builder.Property(x => x.IBAN).HasMaxLength(255); 
        builder.Property(x => x.SWIFT).HasMaxLength(255); 
        builder.Property(x => x.Notes).HasMaxLength(255);
        builder.Property(x => x.Isfinalized);

        builder.HasOne(x => x.Supplier)
            .WithMany(x => x.PurchaseInvoices) // Explicitly defining the relationship
            .HasForeignKey(x => x.SupplierId);

        builder.Ignore(e => e.DomainEvents);
    }
}


