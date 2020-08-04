using System.Collections.Generic;
using System.Threading.Tasks;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Core.Services.Cart
{
    /// <summary>
    /// Campaign service
    /// </summary>
    public interface ICampaignService
    {
        /// <summary>
        /// Calculate campaign discounts
        /// </summary>
        /// <param name="cart">Shopping Cart</param>
        /// <returns>ShoppingCart result</returns>
        Task<ShoppingCart> CalculateAsync(ShoppingCart cart);

        /// <summary>
        /// Deleted a campaign
        /// </summary>
        /// <param name="campaign">Campaign</param>
        Task<int> DeleteCampaignAsync(Campaign campaign);

        /// <summary>
        /// Gets all campaigns
        /// </summary>
        /// <returns>Campaigns</returns>
        Task<IList<Campaign>> GetAllCampaignsAsync();

        /// <summary>
        /// Gets a campaign by identifier
        /// </summary>
        /// <param name="campaignId">Campaign identifier</param>
        /// <returns>Campaign</returns>
        Task<Campaign> GetCampaignByIdAsync(int campaignId);

        /// <summary>
        /// Gets campaign discount
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="totalPrice"></param>
        /// <returns>Campaign Discount</returns>
        decimal GetCampaignDiscount(Campaign campaign, decimal totalPrice);

        /// <summary>
        /// Gets all campaigns belong to categoryId
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Campaigns</returns>
        Task<IList<Campaign>> GetCampaignsByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Gets all shopping cart items belong to categoryId
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>ShoppingCartItems</returns>
        Task<IList<ShoppingCartItem>> GetShoppingCartItemsByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Gets total price of shopping cartitems
        /// </summary>
        /// <param name="items"></param>
        /// <returns>Total Price</returns>
        decimal GetTotalPrice(IList<ShoppingCartItem> items);

        /// <summary>
        /// Gets count of shopping cartitems
        /// </summary>
        /// <param name="items"></param>
        /// <returns>Count</returns>
        decimal GetItemsCount(IList<ShoppingCartItem> items);

        /// <summary>
        /// Gets total price of shopping cartitems by using category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Total Price</returns>
        Task<decimal> GetTotalPriceByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Inserts a campaign
        /// </summary>
        /// <param name="campaign">Campaign</param>        
        Task<int> InsertCampaignAsync(Campaign campaign);

        /// <summary>
        /// Check the campaign is applicable or not
        /// </summary>
        /// <param name="campaign">Campaign</param>    
        /// <param name="cart">ShoppingCart</param>   
        /// <returns>Is Applicable</returns>  
        Task<bool> IsCampaignApplicableAsync(Campaign campaign);

        /// <summary>
        /// Updates a campaign
        /// </summary>
        /// <param name="campaign">Campaign</param>
        Task<int> UpdateCampaignAsync(Campaign campaign);
    }
}
