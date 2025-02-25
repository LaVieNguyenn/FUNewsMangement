using System.Diagnostics;
using Team7MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Team7MVC.BLL.Services.NewsArticleService;
using Team7MVC.BLL.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Team7MVC.DAL.DTOs;

namespace Team7MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly INewArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;

        public NewsController(ILogger<NewsController> logger, INewArticleService newArticleService, ICategoryService categoryService)
        {
            _logger = logger;
            _newsArticleService = newArticleService;
            _categoryService = categoryService;
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
                NewsTitle = n.NewsTitle,
                NewsStatus = n.NewsStatus
            }).ToList();

            return View(newsList);
        }

        [Authorize(Roles = "Admin, Staff")]
        [HttpGet]
        public IActionResult ManageNews()
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
                NewsTitle = n.NewsTitle,
                NewsStatus = n.NewsStatus
            }).ToList();
            var categories = _categoryService.GetAllAsync();
            return View(new ManageNewsViewModel
            {
                NewArticles = newsList,
                Categories = categories.Result,
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateNews(ManageNewsViewModel news)
        {
           await _newsArticleService.CreateNews(new NewsArticleUpdateDTO { 
               Headline = news.Headline,
               CreatedById = news.CreatedById.Value,
               CategoryId = news.CategoryId,    
               CreatedDate = DateTime.Now,  
               NewsContent = news.NewsContent,
               NewsSource = news.NewsSource,
               NewsStatus = news.NewsStatus,
               NewsTitle = news.NewsTitle,
               UpdatedById = news.UpdatedById
           });
            return RedirectToAction("ManageNews");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNews(NewsArticleUpdateDTO news)
        {
            await _newsArticleService.UpdateNews(new NewsArticleUpdateDTO
            {
                NewsArticleId = news.NewsArticleId,
                Headline = news.Headline,
                CreatedById = news.CreatedById,
                CategoryId = news.CategoryId,
                CreatedDate = DateTime.Now,
                NewsContent = news.NewsContent,
                NewsSource = news.NewsSource,
                NewsStatus = news.NewsStatus,
                NewsTitle = news.NewsTitle,
                UpdatedById = news.CreatedById
            });
            return RedirectToAction("ManageNews");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNews(int id)
        {
            await _newsArticleService.Delete(id);
            return RedirectToAction("ManageNews");
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
