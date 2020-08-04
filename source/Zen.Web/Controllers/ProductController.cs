using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Zen.Core.Services.Catalog;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(ILogger<ProductController> logger,
            IProductService productService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllProductsAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            if (!id.HasValue)
                return BadRequest();

            var product = await _productService.GetProductByIdAsync(id.Value);

            if (product is null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            if (model is null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var category = await _categoryService.GetCategoryByIdAsync(model.CategoryId);

                if (category is null)
                    return NotFound();

                await _productService.InsertProductAsync(model);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            if (!id.HasValue)
                return BadRequest();

            var product = await _productService.GetProductByIdAsync(id.Value);

            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,CategoryId,CratedOn")] Product model)
        {
            if (model is null)
                return BadRequest();

            if (!int.Equals(id, model.Id))
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateProductAsync(model);
                }
                catch { }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            if (!id.HasValue)
                return BadRequest();

            var product = await _productService.GetProductByIdAsync(id.Value);

            if (product is null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            await _productService.DeleteProductAsync(product);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Error { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
