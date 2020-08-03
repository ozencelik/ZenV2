namespace Zen.Data.Entities
{
    public class Category : BaseEntity
    {
        public string Title { get; set; }

        public int ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }
    }
}
