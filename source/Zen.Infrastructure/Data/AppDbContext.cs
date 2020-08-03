using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Zen.Data.Entities;

namespace Zen.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().HasKey(e => e.Id);
            modelBuilder.Entity<Category>().Property(e => e.Title);
            modelBuilder.Entity<Category>().Property(e => e.CreatedOn);

            /*
            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCart");
            modelBuilder.Entity<ShoppingCart>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<ShoppingCart>().HasKey("Id");
            modelBuilder.Entity<ShoppingCart>().Property<int>("CustomerId");
            modelBuilder.Entity<ShoppingCart>().HasIndex("CustomerId");
            modelBuilder.Entity<ShoppingCart>().Property<int>("ProductId");
            modelBuilder.Entity<ShoppingCart>().HasIndex("ProductId");
            modelBuilder.Entity<ShoppingCart>().Property<decimal>("Price");
            modelBuilder.Entity<ShoppingCart>().Property<int>("Quantity");
            modelBuilder.Entity<ShoppingCart>().Property<DateTime>("CreatedAt");

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().HasKey("Id");
            modelBuilder.Entity<Product>().Property<string>("Name");
            modelBuilder.Entity<Product>().Property<string>("Description");
            modelBuilder.Entity<Product>().Property<decimal>("Price");
            modelBuilder.Entity<Product>().Property<int>("StockQuantity");
            modelBuilder.Entity<Product>().Property<DateTime>("CreatedAt");
            */
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
