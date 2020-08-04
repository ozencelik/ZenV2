using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zen.Data.Entities;

namespace Zen.Core.Services.Cart
{
    /// <summary>
    /// Campaign service
    /// </summary>
    public interface ICampaignService
    {
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
        /// Gets all campaigns belong to categoryId
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Campaigns</returns>
        Task<IList<Campaign>> GetCampaignsByCategoryIdAsync(int categoryId);

        /// <summary>
        /// Inserts a campaign
        /// </summary>
        /// <param name="campaign">Campaign</param>        
        Task<int> InsertCampaignAsync(Campaign campaign);

        /// <summary>
        /// Updates a campaign
        /// </summary>
        /// <param name="campaign">Campaign</param>
        Task<int> UpdateCampaignAsync(Campaign campaign);
    }
}
