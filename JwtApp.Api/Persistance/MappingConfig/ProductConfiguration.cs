using JwtApp.Api.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtApp.Api.Persistance.MappingConfig
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x=>x.Category).WithMany(x => x.Products).HasForeignKey(x=>x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
