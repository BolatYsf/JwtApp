using JwtApp.Api.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtApp.Api.Persistance.MappingConfig
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasOne(x=>x.AppRole).WithMany(x=>x.AppUsers).HasForeignKey(x=>x.AppRoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
