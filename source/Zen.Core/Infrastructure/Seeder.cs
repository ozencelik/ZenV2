using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zen.Data.Entities;

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

            dbContext.Category.Add(new Category { Title = "Food" });
            dbContext.Category.Add(new Category { Title = "Technology", ParentCategoryId = 1 });
            dbContext.Category.Add(new Category { Title = "Phone", ParentCategoryId = 11 });
            dbContext.Category.Add(new Category { Title = "Computer", ParentCategoryId = 11 });
            dbContext.Category.Add(new Category { Title = "Shoe" });

            dbContext.Product.Add(new Product { Title = "Hamburger", Price = 15, CategoryId = 1});
            dbContext.Product.Add(new Product { Title = "Samsung Galaxy", Price = 1599, CategoryId = 21 });
            dbContext.Product.Add(new Product { Title = "iMac", Price = 2099, CategoryId = 31 });
            dbContext.Product.Add(new Product { Title = "Charge Cable", Price = 30, CategoryId = 11 });
            dbContext.Product.Add(new Product { Title = "Nike - AirMax", Price = 15, CategoryId = 1 });

            dbContext.SaveChanges();
        }
    }
}
