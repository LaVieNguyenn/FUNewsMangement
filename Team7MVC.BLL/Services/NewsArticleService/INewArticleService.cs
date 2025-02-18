using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;
using Team7MVC.DAL.Models;

namespace Team7MVC.BLL.Services.NewsArticleService
{
    public interface INewArticleService
    {
        public Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync();
        public Task<IEnumerable<NewsArticleDTO>> GetAllNewestNewsAsync();
        public Task<IEnumerable<NewsArticleDTO>> GetAllNewestNewsByCategoryNameAsync(string categoryName, int max);
    }
}
