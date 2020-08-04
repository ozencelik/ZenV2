using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zen.Core.Services.Cart;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Web.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ILogger<CampaignController> _logger;
        private readonly ICampaignService _campaignService;

        public CampaignController(ILogger<CampaignController> logger,
            ICampaignService campaignService)
        {
            _logger = logger;
            _campaignService = campaignService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _campaignService.GetAllCampaignsAsync());
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Error { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
