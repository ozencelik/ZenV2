using System;
using System.Collections.Generic;
using System.Text;

namespace Zen.Data.Entities
{
    public class ShoppingCartItem : BaseEntity
    {
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscount { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}
