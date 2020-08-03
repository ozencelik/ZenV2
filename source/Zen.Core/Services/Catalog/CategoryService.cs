using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zen.Core.Infrastructure;
using Zen.Data.Entities;

namespace Zen.Core.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _dbContext;

        public CategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> DeleteCategoryAsync(Category category)
        {
            _dbContext.Category.Remove(category);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Category>> GetAllCategoriesAsync()
        {
            return await _dbContext.Category.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _dbContext.Category
                .FirstOrDefaultAsync(m => m.Id == categoryId);
        }

        public async Task<int> InsertCategoryAsync(Category category)
        {
            _dbContext.Add(category);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateCategoryAsync(Category category)
        {
            _dbContext.Update(category);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
