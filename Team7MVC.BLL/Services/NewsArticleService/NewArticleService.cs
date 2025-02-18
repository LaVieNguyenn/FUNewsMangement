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
        public async Task<IEnumerable<NewsArticleDTO>> GetAllNewestNewsAsync()
        {
            return _repository.GetAllNewestAriticlesAsync().Result.Take(5);
        }

        public Task<IEnumerable<NewsArticleDTO>> GetAllNewestNewsByCategoryNameAsync(string categoryName, int max)
        {
            return _repository.GetAllNewestAriticlesAsyncByAsync(categoryName, max);
        }

        public Task<IEnumerable<NewsArticle>> GetNewsArticlesAsync()
        {
            return _repository.GetAllNewsArticlesAsync();
        }
        public async Task<IEnumerable<NewsArticle>> GetNewsHistoryByStaffIdAsync(int staffId)
        {
            return await _repository.GetNewsHistoryByStaffIdAsync(staffId);
        }
    }
}
