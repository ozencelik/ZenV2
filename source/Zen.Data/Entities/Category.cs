using System.Collections.Generic;

namespace Zen.Data.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public string Title { get; set; }

        public int ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }

        public IList<Product> Products { get; set; }
    }
}
