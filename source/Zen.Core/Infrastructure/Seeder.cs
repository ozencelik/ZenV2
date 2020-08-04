using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Core.Infrastructure
{
    public static class Seeder
    {
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
            foreach (var item in dbContext.Category)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();

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

            dbContext.Product.Add(new Product { Title = "Hamburger", Price = 15, CategoryId = 1});
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

            dbContext.SaveChanges();
        }
    }
}
