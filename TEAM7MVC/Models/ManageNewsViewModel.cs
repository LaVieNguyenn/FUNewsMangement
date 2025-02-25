using Team7MVC.DAL.Models;

namespace Team7MVC.Models
{
    public class ManageNewsViewModel
    {
        public int? NewsArticleId { get; set; }

        public string? NewsTitle { get; set; } = null!;

        public string? Headline { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string NewsContent { get; set; } = null!;

        public string? NewsSource { get; set; }

        public int? CategoryId { get; set; }

        public string? NewsStatus { get; set; } = null!;

        public int? CreatedById { get; set; }

        public int? UpdatedById { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public List<NewArticleViewModel> NewArticles { get; set; }  
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
