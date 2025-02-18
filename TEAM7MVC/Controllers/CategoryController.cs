using Microsoft.AspNetCore.Mvc;
using Team7MVC.BLL.Services.CategoryService;
using Team7MVC.DAL.Models;
using Team7MVC.Models;

namespace Team7MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
       
        public async Task<IActionResult> CreateCategory(CategoryViewModel category)
        {
            // Debugging - Log received values
            Console.WriteLine($"RAW FORM DATA: {Request.Form["IsActive"]}");
            Console.WriteLine($"Received Category: {category.CategoryName}, ParentCategoryId: {category.ParentCategoryId}, IsActive: {category.IsActive}");


            Console.WriteLine("Submitted Model:");
            Console.WriteLine($"Category Name: {category.CategoryName}");
            Console.WriteLine($"Category Description: {category.CategoryDescription}");
            Console.WriteLine($"Parent Category Id: {category.ParentCategoryId}");
            Console.WriteLine($"Is Active: {category.IsActive}");
            Console.WriteLine($"ModelState.IsValid ::  {ModelState.IsValid}");
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                // Ensure ParentCategoryId is null when "None" is selected
                if (string.IsNullOrEmpty(Request.Form["ParentCategoryId"]))
                {
                    category.ParentCategoryId = null;
                }

                // Manually parse IsActive
                

                Console.WriteLine($"Saving Category: {category.CategoryName}, ParentCategoryId: {category.ParentCategoryId}, IsActive: {category.IsActive}");
                
                var newCategoryId = await _categoryService.CreateCategoryAsync(new  Category {
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription,
                    ParentCategoryId = category.ParentCategoryId,
                    IsActive = category.IsActive,
                });
                
                TempData["SuccessMessage"] = "Category created successfully!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Error creating category!";
            return RedirectToAction("Index");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
