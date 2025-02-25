using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;
using Team7MVC.DAL.Models;
using Team7MVC.DAL.Repositories;

namespace Team7MVC.BLL.Services.NewsArticleService
{
    public class NewArticleService : INewArticleService
    {
        private INewsArticleRepository _repository;
        public NewArticleService(INewsArticleRepository repository)
        {
            _repository = repository;
        }

        public Task CreateNews(NewsArticleUpdateDTO newsArticle) => _repository.CreateNews(newsArticle);

        public Task Delete(int id) => _repository.Delete(id);

        public  Task<IEnumerable<NewsArticleDTO>> GetAllNewestNewsAsync()
        {
            return _repository.GetAllNewestAriticlesAsync();
        }

        public Task<IEnumerable<NewsArticleDTO>> GetAllNewestNewsByCategoryNameAsync(string categoryName, int max)
        {
            return _repository.GetAllNewestAriticlesAsyncByAsync(categoryName, max);
        }

        public Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync()
        {
            return _repository.GetAllNewsArticlesAsync();
        }

        public Task UpdateNews(NewsArticleUpdateDTO newsArticle) => _repository.UpdateNews(newsArticle);
    }
}
