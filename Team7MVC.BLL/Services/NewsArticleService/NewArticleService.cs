using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<NewsArticle>> GetAllNewestNewsAsync()
        {
            return _repository.GetAllNewestAriticlesAsync().Result.Take(5);
        }

        public Task<IEnumerable<NewsArticle>> GetAllNewestNewsByCategoryNameAsync(string categoryName, int max)
        {
            return _repository.GetAllNewestAriticlesAsyncByAsync(categoryName, max);
        }

        public Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync()
        {
            return _repository.GetAllNewsArticlesAsync();
        }
    }
}
