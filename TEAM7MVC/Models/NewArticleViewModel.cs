namespace Team7MVC.Models
{
    public class NewHistoryViewModel
    {
        public int NewsArticleId { get; set; }

        public string NewsTitle { get; set; } = null!;

        public string? Headline { get; set; }

        public DateTime CreatedDate { get; set; }

        public string NewsContent { get; set; } = null!;

        public string? NewsSource { get; set; }
         
        public string Category { get; set; }

        public string CreatedBy { get; set; }
    }
}
