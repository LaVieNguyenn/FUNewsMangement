using Team7MVC.BLL.Services.CategoryService;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Transactions;
using Team7MVC.BLL.Services.NewsArticleService;
using Team7MVC.BLL.Services.SystemAccountService;
using Team7MVC.DAL.DAOs;
using Team7MVC.DAL.DAOs.CategoryDAO;
using Team7MVC.DAL.DAOs.NewArticleDAO;
using Team7MVC.DAL.DAOs.SystemAccountDAO;
using Team7MVC.DAL.Repositories;

namespace Team7MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Authentication/Login";
                    options.AccessDeniedPath = "/";
                });
            builder.Services.AddHttpContextAccessor();
            //New Article
            builder.Services.AddSingleton<INewArticleDAO, NewArticleDAO>();
            builder.Services.AddSingleton<INewsArticleRepository, NewsArticleRepository>();
            builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
            builder.Services.AddSingleton<ICatogeryDAO, CategoryDAO>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<INewArticleService, NewArticleService>();
            // Report Service

            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<IReportService, ReportService>();    
           
            //AccountService
            builder.Services.AddSingleton<ISystemAccountDAO, SystemAccountDAO>();
            builder.Services.AddSingleton<ISystemAccountRepository, SystemAccountRepository>();
            builder.Services.AddScoped<ISystemAccountService, SystemAccountService>();
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
