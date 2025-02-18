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
        public Task<IEnumerable<NewsArticle>> GetAllNewestNewsAsync()
        {
            return _repository.GetAllNewestAriticlesAsync();    
        }

        public Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync()
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<NewsArticle>> GetAllNewsArticlesAsync()
        {
            return _repository.GetAllNewsArticlesAsync();
        }
    }
}
