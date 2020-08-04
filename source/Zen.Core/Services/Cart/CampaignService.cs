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
    public class CampaignService : ICampaignService
    {
        private readonly AppDbContext _dbContext;
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;

        public CampaignService(AppDbContext dbContext,
            IShoppingCartService shoppingCartService,
            IProductService productService)
        {
            _dbContext = dbContext;
            _shoppingCartService = shoppingCartService;
            _productService = productService;
        }

        public async Task<ShoppingCart> CalculateAsync(ShoppingCart cart)
        {
            if (cart is null)
                cart = new ShoppingCart();

            var campaigns = await _dbContext.Campaign.ToListAsync();

            foreach (var campaign in campaigns)
            {
                if (await IsCampaignApplicableAsync(campaign))
                {
                    var totalPrice = await GetTotalPriceByCategoryIdAsync(campaign.CategoryId);
                    var discount = GetCampaignDiscount(campaign, totalPrice);

                    //Set discount values
                    cart.CampaignDiscount += discount;
                    cart.CartTotalAfterDiscounts -= discount;
                }
            }

            return cart;
        }

        public async Task<int> DeleteCampaignAsync(Campaign campaign)
        {
            _dbContext.Campaign.Remove(campaign);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Campaign>> GetAllCampaignsAsync()
        {
            return await _dbContext.Campaign.ToListAsync();
        }

        public async Task<Campaign> GetCampaignByIdAsync(int campaignId)
        {
            return await _dbContext.Campaign
               .FirstOrDefaultAsync(m => m.Id == campaignId);
        }

        public decimal GetCampaignDiscount(Campaign campaign, decimal totalPrice)
        {
            if (campaign is null)
                return decimal.Zero;

            if (totalPrice <= decimal.Zero)
                return decimal.Zero;

            decimal discount;

            switch (campaign.DiscountType)
            {
                case DiscountType.Amount:
                    discount = campaign.DiscountAmount;
                    break;
                case DiscountType.Rate:
                default:
                    discount = Math.Round((totalPrice * campaign.DiscountAmount) / 100, 2);
                    break;
            }

            return discount;
        }

        public async Task<IList<Campaign>> GetCampaignsByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Campaign
                .Where(m => m.CategoryId == categoryId)?.ToListAsync();
        }

        public decimal GetItemsCount(IList<ShoppingCartItem> items)
        {
            if (items is null)
                return decimal.Zero;

            return items.Sum(i => i.Quantity);
        }

        public async Task<IList<ShoppingCartItem>> GetShoppingCartItemsByCategoryIdAsync(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId);

            if (products is null)
                return null;

            List<ShoppingCartItem> items = new List<ShoppingCartItem>();
            foreach (var product in products)
            {
                var item = _shoppingCartService.GetShoppingCartItemByProductId(product.Id);

                if (!(item is null))
                    items.Add(item);
            }

            return items;
        }

        public decimal GetTotalPrice(IList<ShoppingCartItem> items)
        {
            if (items is null)
                return decimal.Zero;

            return items.Sum(i => i.TotalPrice);
        }

        public async Task<decimal> GetTotalPriceByCategoryIdAsync(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId);

            if (products is null)
                return decimal.Zero;

            decimal totalPrice = decimal.Zero;
            foreach (var product in products)
            {
                var item = _shoppingCartService.GetShoppingCartItemByProductId(product.Id);

                if (!(item is null))
                    totalPrice += item.TotalPrice;
            }

            return totalPrice;
        }

        public async Task<int> InsertCampaignAsync(Campaign campaign)
        {
            _dbContext.Add(campaign);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsCampaignApplicableAsync(Campaign campaign)
        {
            bool applicable = false;

            if (campaign is null)
                return applicable;

            //Check items count enough and totalPrice is greater than discount to apply the campaign.
            var items = await GetShoppingCartItemsByCategoryIdAsync(campaign.CategoryId);

            var totalPrice = GetTotalPrice(items);

            return applicable = ((GetItemsCount(items) >= campaign.MinItemCount)
                && (totalPrice > GetCampaignDiscount(campaign, totalPrice)));
        }

        public async Task<int> UpdateCampaignAsync(Campaign campaign)
        {
            _dbContext.Update(campaign);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
