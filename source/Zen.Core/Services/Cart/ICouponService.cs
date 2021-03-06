﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Core.Services.Cart
{
    /// <summary>
    /// Coupon service
    /// </summary>
    public interface ICouponService
    {
        /// <summary>
        /// Calculates a coupon discount
        /// </summary>
        /// <param name="coupon">Coupon</param>
        /// <param name="cart">Shopping Cart</param>
        /// <returns>ShoppingCart result</returns>
        ShoppingCart ApplyCoupon(Coupon coupon, ShoppingCart cart);

        /// <summary>
        /// Calculate coupon discounts
        /// </summary>
        /// <param name="cart">Shopping Cart</param>
        /// <returns>ShoppingCart result</returns>
        Task<ShoppingCart> CalculateAsync(ShoppingCart cart);

        /// <summary>
        /// Deleted a coupon
        /// </summary>
        /// <param name="coupon">Coupon</param>
        Task<int> DeleteCouponAsync(Coupon coupon);

        /// <summary>
        /// Gets all coupons
        /// </summary>
        /// <returns>Coupons</returns>
        Task<IList<Coupon>> GetAllCouponsAsync();

        /// <summary>
        /// Gets a coupon by identifier
        /// </summary>
        /// <param name="couponId">Coupon identifier</param>
        /// <returns>Coupon</returns>
        Task<Coupon> GetCouponByIdAsync(int couponId);

        /// <summary>
        /// Gets coupon discount
        /// </summary>
        /// <param name="coupon"></param>
        /// <param name="cartTotal"></param>
        /// <returns>Coupon Discount</returns>
        decimal GetCouponDiscount(Coupon coupon, decimal cartTotal);

        /// <summary>
        /// Inserts a coupon
        /// </summary>
        /// <param name="coupon">Coupon</param>        
        Task<int> InsertCouponAsync(Coupon coupon);

        /// <summary>
        /// Check the coupon is applicable or not
        /// </summary>
        /// <param name="coupon">Coupon</param>
        /// <returns>Is Applicable</returns>  
        bool IsCouponApplicable(Coupon coupon, ShoppingCart cart);

        /// <summary>
        /// Updates a coupon
        /// </summary>
        /// <param name="coupon">Coupon</param>
        Task<int> UpdateCouponAsync(Coupon coupon);
    }
}
