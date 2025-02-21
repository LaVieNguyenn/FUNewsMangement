using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Team7MVC.BLL.Services.SystemAccountService;
using Team7MVC.DAL.DTOs;
using Team7MVC.Models;

namespace Team7MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageAccountController : Controller
    {
        private readonly ISystemAccountService _services;
        public ManageAccountController(ISystemAccountService systemAccountServices)
        {
            _services = systemAccountServices;
        }
        [HttpGet]
        public async Task<IActionResult> AllAccount()
        {
            var systemAccounts = await _services.GetAllAccounts();

            var manageAccounts = systemAccounts.Select(acc => new ManageAccountModel
            {
                AccountID = acc.AccountId,
                AccountName = acc.AccountName,
                AccountEmail = acc.AccountEmail,
                AccountRole = acc.AccountRole
            }).ToList();

            return View(manageAccounts);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(int accountID)
        {
            try
            {
                await _services.DeleteAccountById(accountID);

                TempData["SuccessMessage"] = "Account deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting account.";
            }

            return RedirectToAction("AllAccount");
        }
        [HttpGet]
        public async Task<IActionResult> ViewAccount(int id)
        {
            var account = await _services.GetAccountById(id);

            if (account == null)
            {
                return NotFound();
            }

            var editAccountViewModel = new ManageAccountModel
            {
                AccountID = account.AccountId,
                AccountName = account.AccountName,
                AccountEmail = account.AccountEmail,
                AccountRole = account.AccountRole
            };

            return View(editAccountViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAccount(ManageAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid input." });
            }

            var accountDto = new SystemAccountDTO
            {
                AccountID = model.AccountID,
                AccountName = model.AccountName,
                AccountEmail = model.AccountEmail,
                AccountRole = model.AccountRole
            };

            var result = await _services.UpdateAccount(accountDto);

            if (result)
            {
                return Json(new { success = true, message = "Account updated successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Error updating account." });
            }
        }
   

    }
}
