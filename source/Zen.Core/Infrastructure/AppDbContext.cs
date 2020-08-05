using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Zen.Data.Entities;

namespace Zen.Core.Infrastructure
{
    public class AppDbContext : DbContext
    {
        #region Fields
        public DbSet<Category> Category { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCart { get; set; }

        public DbSet<Campaign> Campaign { get; set; }

        public DbSet<Coupon> Coupon { get; set; }
        #endregion

        #region Ctor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>().HasKey(e => e.Id);
            modelBuilder.Entity<Category>().Property(e => e.Title);
            modelBuilder.Entity<Category>().Property(e => e.ParentCategoryId);
            modelBuilder.Entity<Category>().HasOne(e => e.ParentCategory);
            modelBuilder.Entity<Category>().Property(e => e.CreatedOn);

            modelBuilder.Entity<Product>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().HasKey(e => e.Id);
            modelBuilder.Entity<Product>().Property(e => e.Title);
            modelBuilder.Entity<Product>().Property(e => e.CategoryId);
            modelBuilder.Entity<Product>().Property(e => e.Price);
            modelBuilder.Entity<Product>().HasOne(e => e.Category);
            modelBuilder.Entity<Product>().Property(e => e.CreatedOn);

            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ShoppingCartItem>().HasKey(e => e.Id);
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.Quantity);
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.TotalPrice);
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.TotalDiscount);
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.ProductId);
            modelBuilder.Entity<ShoppingCartItem>().HasOne(e => e.Product);
            modelBuilder.Entity<ShoppingCartItem>().Property(e => e.CreatedOn);

            modelBuilder.Entity<Campaign>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Campaign>().HasKey(e => e.Id);
            modelBuilder.Entity<Campaign>().Property(e => e.Title);
            modelBuilder.Entity<Campaign>().Property(e => e.DiscountAmount);
            modelBuilder.Entity<Campaign>().Property(e => e.DiscountType);
            modelBuilder.Entity<Campaign>().Property(e => e.MinItemCount);
            modelBuilder.Entity<Campaign>().Property(e => e.CategoryId);
            modelBuilder.Entity<Campaign>().HasOne(e => e.Category);
            modelBuilder.Entity<Campaign>().Property(e => e.CreatedOn);

            modelBuilder.Entity<Coupon>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Coupon>().HasKey(e => e.Id);
            modelBuilder.Entity<Coupon>().Property(e => e.Title);
            modelBuilder.Entity<Coupon>().Property(e => e.DiscountAmount);
            modelBuilder.Entity<Coupon>().Property(e => e.DiscountType);
            modelBuilder.Entity<Coupon>().Property(e => e.MinPurchase);
            modelBuilder.Entity<Coupon>().Property(e => e.CreatedOn);

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
        #endregion
    }
}
