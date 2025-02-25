using Team7MVC.DAL.DTOs;

namespace Team7MVC.Models
{
    public class NewsViewModels
    {
        List<NewArticleViewModel> Articles { get; set; } = new List<NewArticleViewModel>();
        List<NewsArticleDTO> NewsByCategories { get; set; } = new List<NewsArticleDTO>();
    }
}
