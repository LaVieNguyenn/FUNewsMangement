using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;
using Team7MVC.DAL.Repositories;

namespace Team7MVC.BLL.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly INewsArticleRepository _newsArticleRepository;

        public ReportService(INewsArticleRepository newsArticleRepository)
        {
            _newsArticleRepository = newsArticleRepository;
        }

        public async Task<IEnumerable<NewsArticleDTO>> GetNewsReportByCategoryAsync(string categoryName)
        {
            // Lấy tất cả bài báo theo danh mục từ repository
            var articles = await _newsArticleRepository.GetAllNewestAriticlesAsyncByAsync(categoryName, int.MaxValue);
            return articles;
        }

        public async Task<IEnumerable<NewsArticleDTO>> GetNewsReportByCategoryAndDateAsync(string categoryName, DateTime startDate, DateTime endDate)
        {
            IEnumerable<NewsArticleDTO> articles;

            // Nếu categoryName null hoặc rỗng, gọi hàm lấy tất cả
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                articles = await _newsArticleRepository.GetAllNewestAriticlesAsync();
            }
            else
            {
                articles = await _newsArticleRepository.GetAllNewestAriticlesAsyncByAsync(categoryName, int.MaxValue);
            }

            // Lọc theo ngày
            return articles.Where(a => a.CreatedDate >= startDate && a.CreatedDate <= endDate);
        }

    }
}
