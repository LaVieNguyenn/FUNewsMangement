using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.DAOs.NewArticleDAO
{
    public interface INewArticleDAO
    {
        Task<IEnumerable<NewsArticleDTO>> GetAllAsync();
        Task<NewsArticle> GetByIdAsync(int id);
        Task<IEnumerable<NewsArticle>> GetAllNewestNews();
        Task<IEnumerable<NewsArticleDTO>> GetAllNewstNewByCategory(string CategoryName, int max);
        Task<IEnumerable<NewsArticle>> GetNewsHistoryByCreatedByIdAsync(int createdById);

    }
}
