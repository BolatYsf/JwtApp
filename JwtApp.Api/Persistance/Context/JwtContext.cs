using JwtApp.Api.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JwtApp.Api.Persistance.Context
{
    public class JwtContext : DbContext
    {
        public JwtContext(DbContextOptions<JwtContext> options) : base(options)
        {

        }
        //nullable 
        public DbSet<Product> Products=>this.Set<Product>();
       
        public DbSet<Category> Categories => this.Set<Category>();

        public DbSet<AppUser> AppUsers => this.Set<AppUser>();

        public DbSet<AppRole> AppRoles => this.Set<AppRole>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
