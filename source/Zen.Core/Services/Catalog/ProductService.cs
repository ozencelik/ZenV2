using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zen.Core.Infrastructure;
using Zen.Data.Entities;

namespace Zen.Core.Services.Catalog
{
    public class ProductService : IProductService
    {
        #region Fields
        private readonly AppDbContext _dbContext;
        #endregion

        #region Ctor
        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<int> DeleteProductAsync(Product product)
        {
            _dbContext.Product.Remove(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _dbContext.Product
                .FirstOrDefaultAsync(m => m.Id == productId);
        }

        public async Task<IList<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Product
                .Where(m => m.CategoryId == categoryId)?.ToListAsync();
        }

        public async Task<int> InsertProductAsync(Product product)
        {
            _dbContext.Add(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            _dbContext.Update(product);
            return await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
