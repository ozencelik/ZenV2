using System;
using System.Collections.Generic;
using System.Text;

namespace Zen.Data.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
