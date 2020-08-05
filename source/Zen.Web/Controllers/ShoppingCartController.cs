﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Zen.Core.Services.Cart;
using Zen.Core.Services.Catalog;
using Zen.Core.Services.Shipment;
using Zen.Data.Entities;
using Zen.Data.Models;

namespace Zen.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        #region Fields
        private const double COST_PER_DELIVERY = 2.49;
        private const double COST_PER_PRODUCT = 1.79;
        private const double FIXED_COST = 2.99;

        private readonly ICampaignService _campaignService;
        private readonly IDeliveryService _deliveryService;
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        #endregion

        #region Ctor
        public ShoppingCartController(IShoppingCartService shoppingCartService,
            IProductService productService,
            ICampaignService campaignService,
            IDeliveryService deliveryService)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _campaignService = campaignService;
            _deliveryService = deliveryService;
        }
        #endregion

        #region Methods
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

        public async Task<IActionResult> CartAsync()
        {
            return View(await _shoppingCartService.GetShoppingCartAsync());
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

        public async Task<IActionResult> Index()
        {
            var items = await _shoppingCartService.GetShoppingCartAsync();

            foreach (var item in items)
            {
                item.Product = await _productService.GetProductByIdAsync(item.ProductId);
            }

            ShoppingCart cart = new ShoppingCart
            {
                Items = items,
                CampaignDiscount = 0,
                CartTotal = items.Sum(i => i.TotalPrice),
                CartTotalAfterDiscounts = items.Sum(i => i.TotalPrice),
                CouponDiscount = 0,
                DeliveryCost = 0
            };

            cart = await _campaignService.CalculateAsync(cart);
            cart.DeliveryCost = await _deliveryService.CalculateDeliveryCostAsync(cart.Items,
                COST_PER_DELIVERY, COST_PER_PRODUCT, FIXED_COST);

            return View(cart);
        }
        #endregion
    }
}
