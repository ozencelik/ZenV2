using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Zen.Core.Services.Cart;
using Zen.Core.Services.Catalog;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Web.Controllers
{
    public class CouponController : Controller
    {
        #region Fields
        private readonly ICouponService _couponService;
        private readonly ILogger<CouponController> _logger;
        #endregion

        #region Ctor
        public CouponController(ILogger<CouponController> logger,
            ICouponService couponService)
        {
            _logger = logger;
            _couponService = couponService;
        }
        #endregion

        #region Methods
        public IActionResult ApplyCoupon(int? id)
        {
            if(id is null)

            var a = 5;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Coupon model)
        {
            if (model is null)
                return BadRequest(nameof(model));

            if (ModelState.IsValid)
            {
                await _couponService.InsertCouponAsync(model);

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

            var coupon = await _couponService.GetCouponByIdAsync(id.Value);

            if (coupon is null)
                return NotFound();

            return View(coupon);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coupon = await _couponService.GetCouponByIdAsync(id);

            await _couponService.DeleteCouponAsync(coupon);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            if (!id.HasValue)
                return BadRequest();


            var coupon = await _couponService.GetCouponByIdAsync(id.Value);

            if (coupon is null)
                return NotFound();

            return View(coupon);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            if (!id.HasValue)
                return BadRequest();

            var coupon = await _couponService.GetCouponByIdAsync(id.Value);

            if (coupon is null)
                return NotFound();

            return View(coupon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id
            , [Bind("Id,Title,DiscountAmount,DiscountType,MinPurchase,CratedOn")] Coupon model)
        {
            if (model is null)
                return BadRequest();

            if (!int.Equals(id, model.Id))
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _couponService.UpdateCouponAsync(model);
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
            return View(await _couponService.GetAllCouponsAsync());
        }
        #endregion
    }
}
