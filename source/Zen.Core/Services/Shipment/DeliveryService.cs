using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zen.Core.Services.Catalog;
using Zen.Data.Entities;

namespace Zen.Core.Services.Shipment
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IProductService _productService;

        public DeliveryService(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<double> CalculateDeliveryCostAsync(IList<ShoppingCartItem> cart,
            double costPerDelivery, double costPerProduct, double fixedCost)
        {
            double result = 0;

            if (cart is null)
                return result;

            var numberOfDeliveries = await GetNumberOfDeliveryAsync(cart);
            var numberOfProducts = cart.Count();

            result = Math.Round((costPerDelivery * numberOfDeliveries)
                + (costPerProduct * numberOfProducts) + fixedCost, 2);

            return result;
        }

        public async Task<int> GetNumberOfDeliveryAsync(IList<ShoppingCartItem> cart)
        {
            List<Product> products = new List<Product>();

            foreach (var item in cart)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);

                if (!(product is null))
                    products.Add(product);
            }

            return products.GroupBy(p => p.CategoryId).Count();
        }
    }
}
