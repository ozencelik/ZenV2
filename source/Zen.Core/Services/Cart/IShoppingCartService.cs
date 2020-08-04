using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Core.Services.Cart
{
    /// <summary>
    /// Shopping cart service
    /// </summary>
    public interface IShoppingCartService
    {
        /// <summary>
        /// Add a product to shopping cart
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantity">Quantity</param>
        /// <returns>Warnings</returns>
        Task<IList<string>> AddItemAsync(Product product, int quantity = 1);

        /// <summary>
        /// Finds a shopping cart item in the cart
        /// </summary>
        /// <param name="shoppingCart">Shopping cart</param>
        /// <param name="product">Product</param>
        /// <returns>Found shopping cart item</returns>
        ShoppingCartItem FindShoppingCartItemInTheCart(IList<ShoppingCartItem> shoppingCart, Product product);

        /// <summary>
        /// Gets shopping cart
        /// </summary>
        /// <returns>Shopping Cart</returns>
        Task<IList<ShoppingCartItem>> GetShoppingCartAsync();

        /// <summary>
        /// Gets shopping cart item by product id
        /// </summary>
        /// <param name="productId">Product</param>
        /// <returns>ShoppingCartItem</returns>
        ShoppingCartItem GetShoppingCartItemByProductId(int productId);
    }
}
