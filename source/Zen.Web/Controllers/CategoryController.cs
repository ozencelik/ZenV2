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
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(ILogger<CategoryController> logger,
            ICategoryService categoryService,
            IProductService productService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllCategoriesAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            if (!id.HasValue)
                return BadRequest();


            var category = await _categoryService.GetCategoryByIdAsync(id.Value);

            if (category is null)
                return NotFound();

            var products = await _productService.GetProductsByCategoryIdAsync(id.Value);

            if (products is null)
                return NotFound();

            category.Products = products;

            return View(category);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.InsertCategoryAsync(model);

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

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CratedOn")] Category model)
        {
            if (model is null)
                return BadRequest();

            if (!int.Equals(id, model.Id))
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategoryAsync(model);
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

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            await _categoryService.DeleteCategoryAsync(category);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Error { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
