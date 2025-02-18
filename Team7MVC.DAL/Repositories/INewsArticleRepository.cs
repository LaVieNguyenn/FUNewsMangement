using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.Repositories
{
    public interface INewsArticleRepository
    {
        Task<IEnumerable<NewsArticle>> GetAllNewsArticlesAsync();   
        Task<NewsArticle> GetNewsArticleByIdAsync(int id);
        Task<IEnumerable<NewsArticle>> GetAllNewestAriticlesAsync();
        Task<IEnumerable<NewsArticle>> GetAllNewestAriticlesAsyncByAsync(string categoryName, int max);
    }
}
