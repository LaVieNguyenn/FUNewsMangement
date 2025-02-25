using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using System.Security.Claims;
using Team7MVC.BLL.Services.SystemAccountService;
using Team7MVC.Models;

namespace Team7MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ISystemAccountService _services;
        public AuthenticationController(ISystemAccountService systemAccountService)
        {
            _services = systemAccountService;
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if(ModelState.IsValid)
            {
                var user = await _services.Login(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Email or Password");
                    return View(model);
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.AccountName),
                    new Claim(ClaimTypes.Email, user.AccountEmail),
                    new Claim(ClaimTypes.Role, user.AccountRole)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else return Redirect("/");
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
