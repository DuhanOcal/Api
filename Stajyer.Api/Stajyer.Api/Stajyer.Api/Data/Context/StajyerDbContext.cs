using Microsoft.EntityFrameworkCore;
using Stajyer.Api.Data.Entities;

namespace Stajyer.Api.Data.Context
{
    public class StajyerDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            dbContextOptionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=calisma;User Id=postgres;Password=$XuvTISTiqdHn;");
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("SELLER");
                entity.Property(x => x.Id).IsRequired();

                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("CUSTOMER");
                entity.Property(x => x.Id).IsRequired();

                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("CATEGORY");
                entity.Property(x => x.Id).IsRequired();

                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("ORDER");
                entity.Property(x => x.Id).IsRequired();

                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("PRODUCT");
                entity.Property(x => x.Id).IsRequired();

                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);
            });

            modelBuilder.Entity<SellerProduct>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("SELLER_PRODUCT");
                entity.Property(x => x.Id).IsRequired();

                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("COMMENTS");
                entity.Property(x => x.Id).IsRequired();

                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);
            });

            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("CARGO");
                entity.Property(x => x.Id).IsRequired();

                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);
            });

            modelBuilder.Entity<Adress>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("ADRESS");
                entity.Property(x => x.Id).IsRequired();

                entity.HasQueryFilter(x => !x.DeletedAt.HasValue);
            });
        }


        public DbSet<Seller> Seller { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<SellerProduct> SellerProduct { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Adress> Adress { get; set; }
    }
}
