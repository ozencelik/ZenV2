using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zen.Core.Infrastructure;
using Zen.Data;
using Zen.Data.Entities;

namespace Zen.Core.Services.Catalog
{
    public class CategoryService : ICategoryService
    {
        #region Fields
        private readonly IRepository<Category> _categoryRepository;
        #endregion

        #region Ctor
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        #endregion

        #region Methods
        public async Task<int> DeleteCategoryAsync(Category category)
        {
            return await _categoryRepository.DeleteAsync(category);
        }

        public async Task<IList<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetByIdAsync(categoryId);
        }

        public async Task<int> InsertCategoryAsync(Category category)
        {
            return await _categoryRepository.InsertAsync(category);
        }

        public async Task<int> UpdateCategoryAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }
        #endregion
    }
}
