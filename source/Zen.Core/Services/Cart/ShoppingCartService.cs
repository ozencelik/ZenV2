using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zen.Core.Infrastructure;
using Zen.Core.Services.Catalog;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Core.Services.Cart
{
    public class ShoppingCartService : IShoppingCartService
    {
        #region Fields
        private readonly AppDbContext _dbContext;
        private readonly IProductService _productService;
        #endregion

        #region Ctor
        public ShoppingCartService(AppDbContext dbContext,
            IProductService productService)
        {
            _dbContext = dbContext;
            _productService = productService;
        }
        #endregion

        #region Methods
        public async Task<IList<string>> AddItemAsync(Product product, int quantity = 1)
        {
            if (product is null)
                return null;

            var cart = await GetShoppingCartAsync();

            var shoppingCartItem = FindShoppingCartItemInTheCart(cart, product);

            if (shoppingCartItem is null)
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

        public decimal GetCartTotal(ShoppingCart cart)
        {
            if (cart is null)
                return decimal.Zero;

            if (cart.Items is null)
                return decimal.Zero;

            return cart.Items.Sum(i => i.TotalPrice);
        }

        public decimal GetCartTotal(IList<ShoppingCartItem> items)
        {
            if (items is null)
                return decimal.Zero;

            return items.Sum(i => i.TotalPrice);
        }

        public decimal GetCartTotalAfterDiscounts(ShoppingCart cart)
        {
            if (cart is null)
                return decimal.Zero;

            return cart.CartTotalAfterDiscounts;
        }

        public async Task<IList<ShoppingCartItem>> GetShoppingCartAsync()
        {
            return await _dbContext.ShoppingCart.ToListAsync();
        }

        public ShoppingCartItem GetShoppingCartItemByProductId(int productId)
        {
            return _dbContext.ShoppingCart.Where(s => s.ProductId == productId).SingleOrDefault();
        }

        public async Task<IList<ShoppingCartItem>> GetShoppingCartItemsByCategoryIdAsync(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId);

            if (products is null)
                return null;

            List<ShoppingCartItem> items = new List<ShoppingCartItem>();
            foreach (var product in products)
            {
                var item = GetShoppingCartItemByProductId(product.Id);

                if (!(item is null))
                    items.Add(item);
            }

            return items;
        }

        public async Task<decimal> GetTotalPriceByCategoryIdAsync(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId);

            if (products is null)
                return decimal.Zero;

            decimal totalPrice = decimal.Zero;
            foreach (var product in products)
            {
                var item = GetShoppingCartItemByProductId(product.Id);

                if (!(item is null))
                    totalPrice += item.TotalPrice;
            }

            return totalPrice;
        }

        public async Task<int> UpdateShoppingCartItemAsync(ShoppingCartItem item)
        {
            if (item is null)
                return 0;

            _dbContext.Update(item);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateShoppingCartItemsDiscountAsync(IList<ShoppingCartItem> items,
            decimal discountAmout, decimal itemsCount)
        {
            if (items is null)
                return false;

            foreach (var item in items)
            {
                item.TotalDiscount = Math.Round(discountAmout / itemsCount, 2);
                await UpdateShoppingCartItemAsync(item);
            }

            return true;
        }
        #endregion
    }
}
