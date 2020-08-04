using System;
using System.Collections.Generic;
using System.Text;
using Zen.Data.Enums;

namespace Zen.Data.Entities
{
    public class Campaign : BaseEntity
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int DiscountAmount { get; set; }

        public DiscountType DiscountType { get; set; }

        public int MinItemCount { get; set; }
    }
}
