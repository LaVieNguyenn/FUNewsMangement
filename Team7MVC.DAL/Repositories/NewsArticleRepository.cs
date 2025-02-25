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

        public Task CreateNews(NewsArticleUpdateDTO newsArticle) => _newArticleDAO.CreateNews(newsArticle);

        public Task Delete(int id) => _newArticleDAO.Delete(id);    

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
            return _newArticleDAO.GetAllNews();
        }

        public Task<NewsArticle> GetNewsArticleByIdAsync(int id)
        {
            return _newArticleDAO.GetByIdAsync(id);
        }

        public Task UpdateNews(NewsArticleUpdateDTO newsArticle) => _newArticleDAO.UpdateNews(newsArticle); 
    }
}