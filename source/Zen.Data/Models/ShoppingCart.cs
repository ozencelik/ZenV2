using System;
using System.Collections.Generic;
using Zen.Data.Entities;

namespace Zen.Data.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }

        public decimal CampaignDiscount { get; set; }

        public decimal CartTotalAfterDiscounts { get; set; }

        public decimal CartTotal { get; set; }

        public decimal CouponDiscount { get; set; }

        public decimal DeliveryCost { get; set; }

        public  IList<ShoppingCartItem> Items { get; set; }


    }
}
