using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zen.Core.Infrastructure;
using Zen.Core.Services.Catalog;
using Zen.Data.Entities;
using Zen.Data.Enums;
using Zen.Data.Models;

namespace Zen.Core.Services.Cart
{
    public class CouponService : ICouponService
    {
        #region Fields
        private readonly AppDbContext _dbContext;
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        #endregion

        #region Ctor
        public CouponService(AppDbContext dbContext,
            IShoppingCartService shoppingCartService,
            IProductService productService)
        {
            _dbContext = dbContext;
            _shoppingCartService = shoppingCartService;
            _productService = productService;
        }
        #endregion

        #region Methods
        public async Task<ShoppingCart> CalculateAsync(ShoppingCart cart)
        {
            if (cart is null)
                cart = new ShoppingCart();

            var coupons = await _dbContext.Coupon.ToListAsync();

            foreach (var coupon in coupons)
            {
                if (IsCouponApplicable(coupon, cart.Items))
                {
                    var cartTotal = GetCartTotal(cart.Items);
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
            _dbContext.Coupon.Remove(coupon);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Coupon>> GetAllCouponsAsync()
        {
            return await _dbContext.Coupon.ToListAsync();
        }

        public decimal GetCartTotal(IList<ShoppingCartItem> items)
        {
            if (items is null)
                return decimal.Zero;

            return items.Sum(i => i.TotalPrice);
        }

        public async Task<Coupon> GetCouponByIdAsync(int couponId)
        {
            return await _dbContext.Coupon
              .FirstOrDefaultAsync(m => m.Id == couponId);
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
            _dbContext.Add(coupon);
            return await _dbContext.SaveChangesAsync();
        }

        public bool IsCouponApplicable(Coupon coupon, IList<ShoppingCartItem> items)
        {
            bool applicable = false;

            if (coupon is null)
                return applicable;

            //Check cart total purchase enough to apply the coupon.
            var cartTotal = GetCartTotal(items);

            return applicable = ((cartTotal >= coupon.MinPurchase)
                && (cartTotal > GetCouponDiscount(coupon, cartTotal)));
        }

        public async Task<int> UpdateCouponAsync(Coupon coupon)
        {
            _dbContext.Update(coupon);
            return await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
