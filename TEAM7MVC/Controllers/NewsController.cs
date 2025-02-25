using System.Diagnostics;
using Team7MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Team7MVC.BLL.Services.NewsArticleService;

namespace Team7MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly INewArticleService _newsArticleService;

        public NewsController(ILogger<NewsController> logger, INewArticleService newArticleService)
        {
            _logger = logger;
            _newsArticleService = newArticleService;
        }

        public IActionResult Index()
        {
            var newsList = _newsArticleService.GetAllNewestNewsAsync().Result.Select(n => new NewArticleViewModel
            {
                NewsArticleId = n.NewsArticleId,
                Category = n.CategoryName,
                Headline = n.Headline,
                CreatedBy = n.AccountName,
                CreatedDate = n.CreatedDate,
                NewsContent = n.NewsContent,
                NewsSource = n.NewsSource,
                NewsTitle = n.NewsTitle 
            }).ToList();

            return View(newsList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
