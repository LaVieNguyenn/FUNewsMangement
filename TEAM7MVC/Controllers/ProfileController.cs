using Team7MVC.DAL.Models;
using Team7MVC.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Team7MVC.Controllers
{
    [Authorize(Roles = "Staff")]
    public class ProfileController : Controller
    {
        private readonly ISystemAccountRepository _accountRepository;

        public ProfileController(ISystemAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // hien thi tt ca nhan
        public async Task<IActionResult> Index()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var account = await _accountRepository.GetAccountByEmailAsync(email);

            if (account == null)
                return NotFound();

            return View(account);
        }

        // hien thi form cap nhat tt
        public async Task<IActionResult> Edit()
        {
            string email = User.FindFirstValue(ClaimTypes.Email);
            var account = await _accountRepository.GetAccountByEmailAsync(email);

            if (account == null)
                return NotFound();

            return View(account);
        }

        // xu ly cap nhat tt
        [HttpPost]
        public async Task<IActionResult> Edit(SystemAccount account)
        {
            if (!ModelState.IsValid)
                return View(account);

            var existingAccount = await _accountRepository.GetAccountByEmailAsync(account.AccountEmail);
            if (existingAccount == null)
                return NotFound();

            // cap nhat tt
            existingAccount.AccountName = account.AccountName;
            await _accountRepository.UpdateAccountAsync(existingAccount);

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Index");
        }
    }
}
