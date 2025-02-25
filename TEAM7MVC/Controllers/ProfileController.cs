using Team7MVC.DAL.Models;
using Team7MVC.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Team7MVC.BLL.Services.SystemAccountService;
using Team7MVC.BLL.Services.NewsArticleService;
using Team7MVC.Models;

namespace Team7MVC.Controllers
{
    [Authorize(Policy = "StaffOnly")]
    public class ProfileController : Controller
    {
        private readonly ISystemAccountService _services;

        public ProfileController(ISystemAccountService systemAccountService)
        {
            _services = systemAccountService;
        }

        // hien thi tt ca nhan
        public async Task<IActionResult> Index()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var account = await _services.GetAccountByEmailAsync(email);

            if (account == null)
                return NotFound();

            var profileViewModel = new ProfileViewModel
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = account.AccountRole
            };

            return View(profileViewModel);
        }

        // hien thi form cap nhat tt
        public async Task<IActionResult> Edit()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var account = await _services.GetAccountByEmailAsync(email);

            if (account == null)
                return NotFound();

            var profileViewModel = new ProfileViewModel
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = account.AccountRole
            };

            return View(profileViewModel);
        }

        // xu ly cap nhat tt
        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel profileViewModel)
        {
            if (!ModelState.IsValid)
                return View(profileViewModel);

            var existingAccount = await _services.GetAccountById(profileViewModel.AccountId);
            if (existingAccount == null)
                return NotFound();

            existingAccount.AccountName = profileViewModel.AccountName;
            existingAccount.AccountEmail = profileViewModel.AccountEmail;
            existingAccount.AccountRole = profileViewModel.AccountRole;

            await _services.UpdateProfileAsync(existingAccount);
                
            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> NewsHistory()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var account = await _services.GetAccountWithNewsHistoryAsync(email);

            if (account == null)
                return RedirectToAction("Login", "Authentication");

            var createdArticles = account.NewsArticleCreatedBies?.Select(article => new NewArticleViewModel
            {
                NewsArticleId = article.NewsArticleId,
                NewsTitle = article.NewsTitle,
                Headline = article.Headline,
                CreatedDate = article.CreatedDate,
                NewsContent = article.NewsContent,
                NewsSource = article.NewsSource ?? "N/A",
                Category = article.Category?.CategoryName ?? "Unknown",
                CreatedBy = account.AccountName
            }).ToList();

            var updatedArticles = account.NewsArticleUpdatedBies?.Select(article => new NewArticleViewModel
            {
                NewsArticleId = article.NewsArticleId,
                NewsTitle = article.NewsTitle,
                Headline = article.Headline,
                CreatedDate = article.ModifiedDate ?? article.CreatedDate,
                NewsContent = article.NewsContent,
                NewsSource = article.NewsSource ?? "N/A",
                Category = article.Category?.CategoryName ?? "Unknown",
                CreatedBy = account.AccountName
            }).ToList();

            var profileViewModel = new ProfileViewModel
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = account.AccountRole,
                CreatedArticles = createdArticles ?? new List<NewArticleViewModel>(),
                UpdatedArticles = updatedArticles ?? new List<NewArticleViewModel>()
            };

            return View(profileViewModel);
        }

    }
}