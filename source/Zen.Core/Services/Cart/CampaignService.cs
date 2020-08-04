using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zen.Core.Infrastructure;
using Zen.Data.Entities;

namespace Zen.Core.Services.Cart
{
    public class CampaignService : ICampaignService
    {
        private readonly AppDbContext _dbContext;

        public CampaignService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<IList<Campaign>> GetCampaignsByCategoryIdAsync(int categoryId)
        {
            return await _dbContext.Campaign
                .Where(m => m.CategoryId == categoryId)?.ToListAsync();
        }

        public async Task<int> InsertCampaignAsync(Campaign campaign)
        {
            _dbContext.Add(campaign);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateCampaignAsync(Campaign campaign)
        {
            _dbContext.Update(campaign);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
