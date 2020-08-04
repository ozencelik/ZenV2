using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zen.Core.Services.Catalog;
using Zen.Core.Services.Cart;
using Zen.Data.Entities;

namespace Zen.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;

        public ShoppingCartController(IShoppingCartService shoppingCartService,
            IProductService productService)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _shoppingCartService.GetShoppingCartAsync());
        }

        public async Task<IActionResult> Create(Product model)
        {
            if (model is null)
                return NotFound(nameof(model));

            var product = await _productService.GetProductByIdAsync(model.Id);

            if (product is null)
                return NotFound(product);

            var item = new ShoppingCartItem()
            {
                ProductId = model.Id,
                Product = product
            };

            return View(item);
        }

        public async Task<IActionResult> AddToCart(ShoppingCartItem model)
        {
            if (model is null)
                return NotFound(nameof(model));

            if (model.Quantity < 1)
                return BadRequest("Item quantitiy must be greater that zero !!!");

            var product = await _productService.GetProductByIdAsync(model.ProductId);

            if (product is null)
                return NotFound(nameof(product));

            var warnings = await _shoppingCartService.AddItemAsync(product, model.Quantity);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CartCount()
        {
            var cart = await _shoppingCartService.GetShoppingCartAsync();

            if (cart is null)
                return Json(new
                {
                    success = false,
                    count = 0
                });

            return Json(new
            {
                success = true,
                count = cart.Count()
            });
        }

        public async Task<IActionResult> CartAsync()
        {
            return View(await _shoppingCartService.GetShoppingCartAsync());
        }
    }
}
