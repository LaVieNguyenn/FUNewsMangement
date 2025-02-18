using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.Repositories
{
    public interface INewsArticleRepository
    {
        Task<IEnumerable<NewsArticle>> GetAllNewsArticlesAsync();   
        Task<NewsArticle> GetNewsArticleByIdAsync(int id);
        Task<IEnumerable<NewsArticleDTO>> GetAllNewestAriticlesAsync();
        Task<IEnumerable<NewsArticleDTO>> GetAllNewestAriticlesAsyncByAsync(string categoryName, int max);
        Task<IEnumerable<NewsArticle>> GetNewsHistoryByStaffIdAsync(int staffId);



    }
}
