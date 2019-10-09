using Microsoft.EntityFrameworkCore;
using BookStore.Api.Entities;
using BookStore.Api.Entities.Enums;
using System.Linq;
using BookStore.Api.DataAccess.Context.EntityConfiguration;

namespace BookStore.Api.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Author> Author { get; set; }

        public DbSet<Publisher> Publisher { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ProductAuthor> ProductAuthor { get; set; }

        public DbSet<ProductPublisher> ProductPublisher { get; set; }

        public DbSet<User> User{ get; set; }

        public DbSet<UserBasket> UserBasket { get; set; }

        public DbSet<UserBasketProduct> UserBasketProduct { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureDecimalPrecisionColumns(modelBuilder);

            ConfigureEntities(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureDecimalPrecisionColumns(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                       .SelectMany(t => t.GetProperties())
                                                       .Where(p => p.ClrType == typeof(decimal)
                                                                   || p.ClrType == typeof(decimal?)))
            {
                property.Relational().ColumnType = "decimal(18, 8)";
            }
        }

        private void ConfigureEntities(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
