using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Zen.Data.Entities;

namespace Zen.Core.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().HasKey(e => e.Id);
            modelBuilder.Entity<Category>().Property(e => e.Title);
            modelBuilder.Entity<Category>().Property(e => e.ParentCategoryId);
            modelBuilder.Entity<Category>().HasOne(e => e.ParentCategory);
            modelBuilder.Entity<Category>().Property(e => e.CreatedOn);


            modelBuilder.Entity<Product>().Property<int>("Id").ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().HasKey("Id");
            modelBuilder.Entity<Product>().Property(e => e.Title);
            modelBuilder.Entity<Product>().Property(e => e.CategoryId);
            modelBuilder.Entity<Product>().Property(e => e.Price);
            modelBuilder.Entity<Product>().HasOne(e => e.Category);
            modelBuilder.Entity<Product>().Property(e => e.CreatedOn);

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
