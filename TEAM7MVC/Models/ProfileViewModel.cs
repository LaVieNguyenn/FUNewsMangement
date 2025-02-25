using Team7MVC.DAL.Models;

namespace Team7MVC.Models
{
    public class ProfileViewModel
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
        public string AccountRole { get; set; } = null!;

        public List<NewArticleViewModel> CreatedArticles { get; set; } = new List<NewArticleViewModel>();
        public List<NewArticleViewModel> UpdatedArticles { get; set; } = new List<NewArticleViewModel>();
    }
}
