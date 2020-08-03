using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Zen.Data.Entities;
using Zen.Data.Models;
using Zen.Infrastructure.Data;

namespace Zen.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly AppDbContext _dbContext;

        public CategoryController(ILogger<CategoryController> logger,
            AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Category.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            if (!id.HasValue)
                return BadRequest();

            var category = await _dbContext.Category
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category is null)
                return NotFound();

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add<Category>(model);
                await _dbContext.SaveChangesAsync();

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

            var category = await _dbContext.Category.FindAsync(id);
            
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
                    _dbContext.Update(model);
                    await _dbContext.SaveChangesAsync();
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

            var category = await _dbContext.Category
                .FirstOrDefaultAsync(m => m.Id == id.Value);

            if (category is null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _dbContext.Category.FindAsync(id);

            _dbContext.Category.Remove(category);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Error { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
