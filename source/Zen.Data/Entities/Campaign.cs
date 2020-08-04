using Zen.Data.Enums;

namespace Zen.Data.Entities
{
    public class Campaign : BaseEntity
    {
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int DiscountAmount { get; set; }

        public DiscountType DiscountType { get; set; }

        public int MinItemCount { get; set; }
    }
}
