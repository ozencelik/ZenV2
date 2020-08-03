using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Zen.Data.Entities;

namespace Zen.Core.Services.Catalog
{
    public interface ICategoryService
    {
        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category">Category</param>
        Task<int> DeleteCategoryAsync(Category category);

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>Categories</returns>
        Task<IList<Category>> GetAllCategoriesAsync();

        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category</returns>
        Task<Category> GetCategoryByIdAsync(int categoryId);

        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="category">Category</param>
        Task<int> InsertCategoryAsync(Category category);

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <param name="category">Category</param>
        Task<int> UpdateCategoryAsync(Category category);
    }
}