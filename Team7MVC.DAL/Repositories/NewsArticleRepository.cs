using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DAOs.NewArticleDAO;
using Team7MVC.DAL.DTOs;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly INewArticleDAO _newArticleDAO;
        public NewsArticleRepository(INewArticleDAO newArticleDAO)
        {
            _newArticleDAO = newArticleDAO;            
        }

        public Task<IEnumerable<NewsArticleDTO>> GetAllNewestAriticlesAsync()
        {
            return _newArticleDAO.GetAllAsync();    
        }

        public Task<IEnumerable<NewsArticleDTO>> GetAllNewestAriticlesAsyncByAsync(string categoryName, int max)
        {
            return _newArticleDAO.GetAllNewstNewByCategory(categoryName, max);
        }

        public Task<IEnumerable<NewsArticle>> GetAllNewsArticlesAsync()
        {
            return _newArticleDAO.GetAllNewestNews();
        }

        public Task<NewsArticle> GetNewsArticleByIdAsync(int id)
        {
            return _newArticleDAO.GetByIdAsync(id);
        }
    }
}
