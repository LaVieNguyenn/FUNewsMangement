using Microsoft.AspNetCore.Mvc;
using Team7MVC.BLL.Services.ReportService;
using Team7MVC.BLL.Services.CategoryService;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Team7MVC.DAL.DTOs;

namespace Team7MVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ICategoryService _categoryService;

        public ReportController(IReportService reportService, ICategoryService categoryService)
        {
            _reportService = reportService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? categoryName, DateTime? startDate, DateTime? endDate)
        {
            // Xử lý null cho startDate và endDate
            DateTime start = startDate ?? DateTime.MinValue;
            DateTime end = endDate ?? DateTime.MaxValue;

            // Nếu categoryName là null, chuyển thành chuỗi rỗng
            string effectiveCategory = categoryName ?? "";

            // Lấy báo cáo từ ReportService
            var reports = await _reportService.GetNewsReportByCategoryAndDateAsync(effectiveCategory, start, end);
            var categories = await _categoryService.GetAllAsync();

            // Tính toán phân phối số lượng bài viết theo Category
            var categoryDistribution = GetCategoryDistribution(reports);

            var model = new
            {
                Reports = reports.ToList(),
                Categories = categories.ToList(),
                SelectedCategory = categoryName,
                StartDate = start,
                EndDate = end,
                CategoryDistribution = categoryDistribution
            };

            return View(model);
        }

        private Dictionary<string, int> GetCategoryDistribution(IEnumerable<NewsArticleDTO> reports)
        {
            // Nhóm các bài báo theo CategoryName và đếm số lượng cho mỗi Category
            return reports.GroupBy(r => r.CategoryName)
                          .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
