﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zen.Core.Infrastructure;
using Zen.Core.Services.Catalog;
using Zen.Data;
using Zen.Data.Entities;
using Zen.Data.Enums;
using Zen.Data.Models;

namespace Zen.Core.Services.Cart
{
    public class CouponService : ICouponService
    {
        #region Fields
        private readonly IRepository<Coupon> _couponRepository;
        private readonly IShoppingCartService _shoppingCartService;
        #endregion

        #region Ctor
        public CouponService(IRepository<Coupon> couponRepository,
            IShoppingCartService shoppingCartService)
        {
            _couponRepository = couponRepository;
            _shoppingCartService = shoppingCartService;
        }
        #endregion

        #region Methods
        public ShoppingCart ApplyCoupon(Coupon coupon, ShoppingCart cart)
        {
            if (coupon is null)
                return default;

            if (cart is null)
                return default;

            if (IsCouponApplicable(coupon, cart))
            {
                var cartTotal = _shoppingCartService.GetCartTotal(cart);
                var discount = GetCouponDiscount(coupon, cartTotal);

                //Set discount values
                cart.CouponDiscount += discount;
                cart.CartTotalAfterDiscounts -= discount;
            }

            return cart;
        }

        public async Task<ShoppingCart> CalculateAsync(ShoppingCart cart)
        {
            if (cart is null)
                cart = new ShoppingCart();

            var coupons = await GetAllCouponsAsync();

            foreach (var coupon in coupons)
            {
                if (IsCouponApplicable(coupon, cart))
                {
                    var cartTotal = _shoppingCartService.GetCartTotal(cart.Items);
                    var discount = GetCouponDiscount(coupon, cartTotal);

                    //Set discount values
                    cart.CouponDiscount += discount;
                    cart.CartTotalAfterDiscounts -= discount;
                }
            }

            return cart;
        }

        public async Task<int> DeleteCouponAsync(Coupon coupon)
        {
            return await _couponRepository.DeleteAsync(coupon);
        }

        public async Task<IList<Coupon>> GetAllCouponsAsync()
        {
            return await _couponRepository.GetAllAsync();
        }

        public async Task<Coupon> GetCouponByIdAsync(int couponId)
        {
            return await _couponRepository.Table
              .FirstOrDefaultAsync(c => c.Id == couponId);
        }

        public decimal GetCouponDiscount(Coupon coupon, decimal cartTotal)
        {
            if (coupon is null)
                return decimal.Zero;

            if (cartTotal <= decimal.Zero)
                return decimal.Zero;

            decimal discount;

            switch (coupon.DiscountType)
            {
                case DiscountType.Amount:
                    discount = coupon.DiscountAmount;
                    break;
                case DiscountType.Rate:
                default:
                    discount = Math.Round((cartTotal * coupon.DiscountAmount) / 100, 2);
                    break;
            }

            return discount;
        }

        public async Task<int> InsertCouponAsync(Coupon coupon)
        {
            return await _couponRepository.InsertAsync(coupon);
        }

        public bool IsCouponApplicable(Coupon coupon, ShoppingCart cart)
        {
            bool applicable = false;

            if (coupon is null)
                return applicable;

            if (cart is null)
                return applicable;

            //Check cart total purchase enough to apply the coupon.
            var cartTotal = _shoppingCartService.GetCartTotal(cart);
            var cartTotalAfterDiscounts = _shoppingCartService.GetCartTotalAfterDiscounts(cart);

            return applicable = ((cartTotal >= coupon.MinPurchase)
                && (cartTotalAfterDiscounts > GetCouponDiscount(coupon, cartTotal)));
        }

        public async Task<int> UpdateCouponAsync(Coupon coupon)
        {
            return await _couponRepository.UpdateAsync(coupon);
        }
        #endregion
    }
}
