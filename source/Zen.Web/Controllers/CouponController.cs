using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Zen.Core.Services.Cart;
using Zen.Core.Services.Catalog;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICampaignService _campaignService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CouponController> _logger;

        public CouponController(ILogger<CouponController> logger,
            ICampaignService campaignService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _campaignService = campaignService;
            _categoryService = categoryService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Campaign model)
        {
            if (model is null)
                return BadRequest(nameof(model));

            if (ModelState.IsValid)
            {
                await _campaignService.InsertCampaignAsync(model);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            if (!id.HasValue)
                return BadRequest();

            var campaign = await _campaignService.GetCampaignByIdAsync(id.Value);

            if (campaign is null)
                return NotFound();

            return View(campaign);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(id);

            await _campaignService.DeleteCampaignAsync(campaign);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            if (!id.HasValue)
                return BadRequest();


            var campaign = await _campaignService.GetCampaignByIdAsync(id.Value);

            if (campaign is null)
                return NotFound();

            var category = await _categoryService.GetCategoryByIdAsync(campaign.CategoryId);

            if (category is null)
                return NotFound();

            campaign.Category = category;

            return View(campaign);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            if (!id.HasValue)
                return BadRequest();

            var campaign = await _campaignService.GetCampaignByIdAsync(id.Value);

            if (campaign is null)
                return NotFound();

            return View(campaign);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id
            , [Bind("Id,Title,CategoryId,DiscountAmount,DiscountType,MinItemCount,CratedOn")] Campaign model)
        {
            if (model is null)
                return BadRequest();

            if (!int.Equals(id, model.Id))
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _campaignService.UpdateCampaignAsync(model);
                }
                catch { }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Error { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index()
        {
            return View(await _campaignService.GetAllCampaignsAsync());
        }
    }
}
