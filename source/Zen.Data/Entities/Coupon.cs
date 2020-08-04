using System;
using System.Collections.Generic;
using System.Text;
using Zen.Data.Enums;

namespace Zen.Data.Entities
{
    public class Coupon : BaseEntity
    {
        public string Title { get; set; }

        public int DiscountAmount { get; set; }

        public decimal MinPurchase { get; set; }

        public DiscountType DiscountType { get; set; }
    }
}
