using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Zen.Data.Entities;

namespace Zen.Core.Services.Catalog
{
    public interface IProductService
    {
        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="product">Product</param>
        Task<int> DeleteProductAsync(Product product);

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>Categories</returns>
        Task<List<Product>> GetAllProductsAsync();

        /// <summary>
        /// Gets a product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        Task<Product> GetProductByIdAsync(int productId);

        /// <summary>
        /// Inserts product
        /// </summary>
        /// <param name="product">Product</param>
        Task<int> InsertProductAsync(Product product);

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="product">Product</param>
        Task<int> UpdateProductAsync(Product product);
    }
}