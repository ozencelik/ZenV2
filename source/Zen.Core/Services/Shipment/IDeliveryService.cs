using System.Collections.Generic;
using System.Threading.Tasks;
using Zen.Data.Entities;

namespace Zen.Core.Services.Shipment
{
    /// <summary>
    /// Delivery service
    /// </summary>
    public interface IDeliveryService
    {
        /// <summary>
        /// Calculate delivery cost
        /// </summary>
        /// <param name="cart">Shopping Cart</param>
        /// <returns>Delivery Cost</returns>
        Task<double> CalculateDeliveryCostAsync(IList<ShoppingCartItem> cart,
            double costPerDelivery, double costPerProduct, double fixedCost);

        /// <summary>
        /// Calculate delivery cost
        /// </summary>
        /// <param name="cart">Shopping Cart</param>
        /// <returns>Number of Delivery</returns>
        Task<int> GetNumberOfDeliveryAsync(IList<ShoppingCartItem> cart);
    }
}
