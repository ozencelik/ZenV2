using System;
using System.Collections.Generic;
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
        /// Gets cart total price of shopping cart
        /// </summary>
        /// <returns>Cart total</returns>
        decimal GetCartTotal(ShoppingCart cart);

        /// <summary>
        /// Gets cart total price of shopping cartitems
        /// </summary>
        /// <param name="items"></param>
        /// <returns>Cart total</returns>
        decimal GetCartTotal(IList<ShoppingCartItem> items);

        /// <summary>
        /// Gets cart total of shopping cart after discounts 
        /// </summary>
        /// <returns>Cart total after dsicounts</returns>
        decimal GetCartTotalAfterDiscounts(ShoppingCart cart);

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

        /// <summary>
        /// Gets shopping cart items by category id
        /// </summary>
        /// <param name="categoryId">Caegory</param>
        /// <returns>ShoppingCartItems</returns>
        Task<IList<ShoppingCartItem>> GetShoppingCartItemsByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Gets total price of products in te cart by category id
        /// </summary>
        /// <param name="categoryId">Caegory</param>
        /// <returns>Total Price</returns>
        Task<decimal> GetTotalPriceByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Update a shopping cart item
        /// </summary>
        /// <param name="item">ShoppingCartItem</param>
        /// <returns>ShoppingCartItem id</returns>
        Task<int> UpdateShoppingCartItemAsync(ShoppingCartItem item);

        /// <summary>
        /// Update a shopping cart item discount amout
        /// </summary>
        /// <param name="items">ShoppingCartItem</param>
        /// <param name="discountAmout">Total dsicount for shoppig cart items</param>
        /// <param name="itemsCount">ShoppingCartItems count</param>
        /// <returns>ShoppingCartItem id</returns>
        Task<bool> UpdateShoppingCartItemsDiscountAsync(IList<ShoppingCartItem> items,
            decimal discountAmout, decimal itemsCount);
    }
}
