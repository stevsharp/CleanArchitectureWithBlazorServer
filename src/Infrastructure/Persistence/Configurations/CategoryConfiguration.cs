
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Blazor.Infrastructure.Persistence.Configurations;

#nullable disable
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasIndex(x => x.Name); 
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Comments).HasMaxLength(500).IsRequired();

        builder.HasMany(c => c.SubCategories)
           .WithMany(sc => sc.Categories)
           .UsingEntity<Dictionary<string, object>>(
               "CategorySubCategory",
               j => j
                   .HasOne<SubCategory>()
                   .WithMany()
                   .HasForeignKey("SubCategoryId")
                   .OnDelete(DeleteBehavior.Cascade),
               j => j
                   .HasOne<Category>()
                   .WithMany()
                   .HasForeignKey("CategoryId")
                   .OnDelete(DeleteBehavior.Cascade),
               j =>
               {
                   j.HasKey("CategoryId", "SubCategoryId");
                   j.ToTable("CategorySubCategory");
               });


        builder.Ignore(e => e.DomainEvents);
    }
}


