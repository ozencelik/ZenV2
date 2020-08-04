using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zen.Core.Infrastructure;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Core.Services.Cart
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly AppDbContext _dbContext;

        public ShoppingCartService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<string>> AddItemAsync(Product product, int quantity = 1)
        {
            if (product is null)
                return null;

            var cart = await GetShoppingCartAsync();

            var shoppingCartItem = FindShoppingCartItemInTheCart(cart, product);

            if(shoppingCartItem is null)
            {
                var newItem = new ShoppingCartItem()
                {
                    ProductId = product.Id,
                    Quantity = quantity,
                    TotalPrice = product.Price * quantity
                };

                _dbContext.Add(newItem);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                shoppingCartItem.Quantity += quantity;
                shoppingCartItem.TotalPrice = shoppingCartItem.Quantity * product.Price;

                await UpdateShoppingCartItemAsync(shoppingCartItem);
            }

            return null;
        }

        public ShoppingCartItem FindShoppingCartItemInTheCart(IList<ShoppingCartItem> shoppingCart, Product product)
        {
            if (shoppingCart == null)
                throw new ArgumentNullException(nameof(shoppingCart));

            if (product == null)
                throw new ArgumentNullException(nameof(product));

            return shoppingCart.Where(s => s.ProductId == product.Id).SingleOrDefault();
        }

        public async Task<IList<ShoppingCartItem>> GetShoppingCartAsync()
        {
            return await _dbContext.ShoppingCart.ToListAsync();
        }

        public ShoppingCartItem GetShoppingCartItemByProductId(int productId)
        {
            return _dbContext.ShoppingCart.Where(s => s.ProductId == productId).SingleOrDefault();
        }

        public async Task<int> UpdateShoppingCartItemAsync(ShoppingCartItem item)
        {
            if (item is null)
                return 0;

            _dbContext.Update(item);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
