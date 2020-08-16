using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zen.Data;
using Zen.Data.Entities;
using Zen.Data.Enums;
using Zen.Data.Models;

namespace Zen.Core.Services.Cart
{
    public class CampaignService : ICampaignService
    {
        #region Fields
        private readonly IRepository<Campaign> _campaignRepository;
        private readonly IShoppingCartService _shoppingCartService;
        #endregion

        #region Ctor
        public CampaignService(IRepository<Campaign> campaignRepository,
            IShoppingCartService shoppingCartService)
        {
            _campaignRepository = campaignRepository;
            _shoppingCartService = shoppingCartService;
        }
        #endregion

        #region Methods
        public async Task<ShoppingCart> CalculateAsync(ShoppingCart cart)
        {
            if (cart is null)
                return default;

            var campaigns = await GetAllCampaignsAsync();

            foreach (var campaign in campaigns)
            {
                if (await IsCampaignApplicableAsync(campaign))
                {
                    var totalPrice = await _shoppingCartService.GetTotalPriceByCategoryIdAsync(campaign.CategoryId);
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
            return await _campaignRepository.DeleteAsync(campaign);
        }

        public async Task<IList<Campaign>> GetAllCampaignsAsync()
        {
            return await _campaignRepository.GetAllAsync();
        }

        public async Task<Campaign> GetCampaignByIdAsync(int campaignId)
        {
            return await _campaignRepository.GetByIdAsync(campaignId);
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
            return await _campaignRepository.Table
                .Where(c => c.CategoryId == categoryId)?.ToListAsync();
        }

        public decimal GetItemsCount(IList<ShoppingCartItem> items)
        {
            if (items is null)
                return decimal.Zero;

            return items.Sum(i => i.Quantity);
        }

        public decimal GetTotalPrice(IList<ShoppingCartItem> items)
        {
            if (items is null)
                return decimal.Zero;

            return items.Sum(i => i.TotalPrice);
        }

        public decimal GetTotalDiscounts(IList<ShoppingCartItem> items)
        {
            if (items is null)
                return decimal.Zero;

            return items.Sum(i => i.TotalDiscount);
        }

        public decimal GetTotalPriceAfterDiscounts(IList<ShoppingCartItem> items)
        {
            if (items is null)
                return decimal.Zero;

            return GetTotalPrice(items) - GetTotalDiscounts(items);
        }

        public async Task<int> InsertCampaignAsync(Campaign campaign)
        {
            return await _campaignRepository.InsertAsync(campaign);
        }

        public async Task<bool> IsCampaignApplicableAsync(Campaign campaign)
        {
            bool applicable = false;

            if (campaign is null)
                return applicable;

            //Check items count enough and totalPrice is greater than discount to apply the campaign.
            var items = await _shoppingCartService.GetShoppingCartItemsByCategoryIdAsync(campaign.CategoryId);

            var totalPrice = GetTotalPrice(items);
            var totalPriceAfterDiscounts = GetTotalPriceAfterDiscounts(items);
            var itemsCount = GetItemsCount(items);
            decimal campaignDiscount = GetCampaignDiscount(campaign, totalPrice);

            applicable = ((itemsCount >= campaign.MinItemCount)
                && (totalPriceAfterDiscounts > campaignDiscount));

            if (applicable)
                await _shoppingCartService.UpdateShoppingCartItemsDiscountAsync(items, campaignDiscount, itemsCount);

            return applicable;
        }

        public async Task<int> UpdateCampaignAsync(Campaign campaign)
        {
            return await _campaignRepository.UpdateAsync(campaign);
        }
        #endregion
    }
}
