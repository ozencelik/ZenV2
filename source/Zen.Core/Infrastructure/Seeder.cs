using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zen.Data.Entities;
using Zen.Data.Enums;
using Zen.Data.Models;

namespace Zen.Core.Infrastructure
{
    public static class Seeder
    {
        #region Methods
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Look for any Category.
                if (dbContext.Category.Any())
                {
                    return;   // DB has been seeded
                }

                PopulateTestData(dbContext);
            }
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
            dbContext.Category.Add(new Category { Title = "A" });
            dbContext.Category.Add(new Category { Title = "B" });
            dbContext.Category.Add(new Category { Title = "C", ParentCategoryId = 11 });
            dbContext.Category.Add(new Category { Title = "D", ParentCategoryId = 11 });
            dbContext.Category.Add(new Category { Title = "E" });
            dbContext.Category.Add(new Category { Title = "F" });
            dbContext.Category.Add(new Category { Title = "G" });
            dbContext.Category.Add(new Category { Title = "H", ParentCategoryId = 51 });
            dbContext.Category.Add(new Category { Title = "I", ParentCategoryId = 51 });
            dbContext.Category.Add(new Category { Title = "İ" });
            dbContext.Category.Add(new Category { Title = "J" });
            dbContext.Category.Add(new Category { Title = "K" });
            dbContext.Category.Add(new Category { Title = "L", ParentCategoryId = 71 });
            dbContext.Category.Add(new Category { Title = "M", ParentCategoryId = 181 });
            dbContext.Category.Add(new Category { Title = "N" });

            dbContext.Product.Add(new Product { Title = "Hamburger", Price = 15, CategoryId = 1 });
            dbContext.Product.Add(new Product { Title = "Samsung Galaxy", Price = 1599, CategoryId = 21 });
            dbContext.Product.Add(new Product { Title = "iMac", Price = 2099, CategoryId = 31 });
            dbContext.Product.Add(new Product { Title = "Charge Cable", Price = 30, CategoryId = 11 });
            dbContext.Product.Add(new Product { Title = "Nike - AirMax", Price = 15, CategoryId = 1 });
            dbContext.Product.Add(new Product { Title = "A", Price = 15, CategoryId = 1 });
            dbContext.Product.Add(new Product { Title = "B", Price = 1599, CategoryId = 21 });
            dbContext.Product.Add(new Product { Title = "C", Price = 2099, CategoryId = 31 });
            dbContext.Product.Add(new Product { Title = "D", Price = 30, CategoryId = 11 });
            dbContext.Product.Add(new Product { Title = "E", Price = 15, CategoryId = 1 });
            dbContext.Product.Add(new Product { Title = "F", Price = 15, CategoryId = 1 });
            dbContext.Product.Add(new Product { Title = "G", Price = 1599, CategoryId = 21 });
            dbContext.Product.Add(new Product { Title = "H", Price = 2099, CategoryId = 31 });
            dbContext.Product.Add(new Product { Title = "I", Price = 30, CategoryId = 11 });
            dbContext.Product.Add(new Product { Title = "İ", Price = 15, CategoryId = 1 });

            dbContext.ShoppingCart.Add(new ShoppingCartItem { ProductId = 1, Quantity = 5, TotalPrice = 75, TotalDiscount = 0 });
            dbContext.ShoppingCart.Add(new ShoppingCartItem { ProductId = 31, Quantity = 3, TotalPrice = 90, TotalDiscount = 0 });
            dbContext.ShoppingCart.Add(new ShoppingCartItem { ProductId = 51, Quantity = 10, TotalPrice = 150, TotalDiscount = 0 });

            dbContext.Campaign.Add(new Campaign { Title = "A", CategoryId = 1, DiscountAmount = 10, DiscountType = DiscountType.Amount, MinItemCount = 5 });
            dbContext.Campaign.Add(new Campaign { Title = "B", CategoryId = 11, DiscountAmount = 5, DiscountType = DiscountType.Amount, MinItemCount = 1 });
            dbContext.Campaign.Add(new Campaign { Title = "C", CategoryId = 11, DiscountAmount = 5, DiscountType = DiscountType.Rate, MinItemCount = 3 });
            dbContext.Campaign.Add(new Campaign { Title = "D", CategoryId = 11, DiscountAmount = 5, DiscountType = DiscountType.Amount, MinItemCount = 4 });
            dbContext.Campaign.Add(new Campaign { Title = "E", CategoryId = 41, DiscountAmount = 5, DiscountType = DiscountType.Rate, MinItemCount = 2 });
            dbContext.Campaign.Add(new Campaign { Title = "F", CategoryId = 41, DiscountAmount = 20, DiscountType = DiscountType.Amount, MinItemCount = 1 });
            dbContext.Campaign.Add(new Campaign { Title = "G", CategoryId = 41, DiscountAmount = 20, DiscountType = DiscountType.Rate, MinItemCount = 2 });
            dbContext.Campaign.Add(new Campaign { Title = "H", CategoryId = 51, DiscountAmount = 20, DiscountType = DiscountType.Amount, MinItemCount = 3 });
            dbContext.Campaign.Add(new Campaign { Title = "I", CategoryId = 51, DiscountAmount = 20, DiscountType = DiscountType.Rate, MinItemCount = 4 });
            dbContext.Campaign.Add(new Campaign { Title = "İ", CategoryId = 51, DiscountAmount = 4, DiscountType = DiscountType.Amount, MinItemCount = 5 });
            dbContext.Campaign.Add(new Campaign { Title = "J", CategoryId = 51, DiscountAmount = 5, DiscountType = DiscountType.Rate, MinItemCount = 1 });
            dbContext.Campaign.Add(new Campaign { Title = "K", CategoryId = 61, DiscountAmount = 8, DiscountType = DiscountType.Amount, MinItemCount = 2 });
            dbContext.Campaign.Add(new Campaign { Title = "L", CategoryId = 61, DiscountAmount = 12, DiscountType = DiscountType.Rate, MinItemCount = 3 });
            dbContext.Campaign.Add(new Campaign { Title = "M", CategoryId = 61, DiscountAmount = 15, DiscountType = DiscountType.Amount, MinItemCount = 4 });
            dbContext.Campaign.Add(new Campaign { Title = "N", CategoryId = 61, DiscountAmount = 6, DiscountType = DiscountType.Rate, MinItemCount = 5 });

            dbContext.Coupon.Add(new Coupon { Title = "A", DiscountAmount = 10, DiscountType = DiscountType.Amount, MinPurchase = 50 });
            dbContext.Coupon.Add(new Coupon { Title = "B", DiscountAmount = 5, DiscountType = DiscountType.Amount, MinPurchase = 35 });
            dbContext.Coupon.Add(new Coupon { Title = "C", DiscountAmount = 5, DiscountType = DiscountType.Rate, MinPurchase = 70 });
            dbContext.Coupon.Add(new Coupon { Title = "D", DiscountAmount = 5, DiscountType = DiscountType.Amount, MinPurchase = 100 });
            dbContext.Coupon.Add(new Coupon { Title = "E", DiscountAmount = 5, DiscountType = DiscountType.Rate, MinPurchase = 200 });

            dbContext.SaveChanges();
        }
        #endregion
    }
}
