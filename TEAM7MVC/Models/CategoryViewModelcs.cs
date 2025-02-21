namespace Team7MVC.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public string? CategoryDescription { get; set; }

        public int? ParentCategoryId { get; set; }

        public bool IsActive { get; set; }

    }
}
