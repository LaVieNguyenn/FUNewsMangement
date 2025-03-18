using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;

namespace Team7MVC.BLL.Services.ReportService
{
    public interface IReportService
    {
        Task<IEnumerable<NewsArticleDTO>> GetNewsReportByCategoryAsync(string categoryName);
        Task<IEnumerable<NewsArticleDTO>> GetNewsReportByCategoryAndDateAsync(string categoryName, DateTime startDate, DateTime endDate);
    }
}
