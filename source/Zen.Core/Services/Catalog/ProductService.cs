using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zen.Core.Infrastructure;
using Zen.Data;
using Zen.Data.Entities;

namespace Zen.Core.Services.Catalog
{
    public class ProductService : IProductService
    {
        #region Fields
        private readonly IRepository<Product> _productRepository;
        #endregion

        #region Ctor
        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region Methods
        public async Task<int> DeleteProductAsync(Product product)
        {
            return await _productRepository.DeleteAsync(product);
        }

        public async Task<IList<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _productRepository.Table
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IList<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _productRepository.Table
                .Where(m => m.CategoryId == categoryId)?.ToListAsync();
        }

        public async Task<int> InsertProductAsync(Product product)
        {
            return await _productRepository.InsertAsync(product);
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateAsync(product);
        }
        #endregion
    }
}
