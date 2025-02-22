using Microsoft.AspNetCore.Mvc;
using Team7MVC.BLL.Services.CategoryService;
using Team7MVC.BLL.Services.NewsArticleService;
using Team7MVC.DAL.Models;
using Team7MVC.Models;

namespace Team7MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly INewArticleService _newsArticleService;
        public CategoryController(ICategoryService categoryService, INewArticleService newsArticleService)
        {
            _categoryService = categoryService;
            _newsArticleService = newsArticleService;
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

                var newCategoryId = await _categoryService.CreateCategoryAsync(new Category
                {
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
        // DELETE CATEGORY
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                // Call the service to delete the category
                await _categoryService.DeleteCategoryAsync(id);
                TempData["SuccessMessage"] = "Category deleted successfully!";
            }
            catch (Exception ex)
            {
                // Handle deletion errors (e.g., category has child categories)
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
        // GET: Show the Edit Category Form
        [HttpGet]
        public async Task<IActionResult> ShowEditCategoryForm(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Json(new
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ParentCategoryId = category.ParentCategoryId,
                IsActive = category.IsActive
            });
        }



        // POST: Save Changes to Category
        [HttpPost]
        public async Task<IActionResult> SaveEditedCategory(CategoryViewModel category)
        {
            Console.WriteLine("Submitted Model:");
            Console.WriteLine($" Category Id: {category.CategoryId}");
            Console.WriteLine($"Category Name: {category.CategoryName}");
            Console.WriteLine($"Category Description: {category.CategoryDescription}");
            Console.WriteLine($"Parent Category Id: {category.ParentCategoryId}");
            Console.WriteLine($"Is Active: {category.IsActive}");
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

                var categoryEntity = await _categoryService.UpdateCategoryAsync(new Category
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    CategoryDescription = category.CategoryDescription,
                    ParentCategoryId = category.ParentCategoryId,
                    IsActive = category.IsActive,
                });
                // Map to entity if necessary



                if (categoryEntity)
                {
                    TempData["SuccessMessage"] = "Category updated successfully!";
                    return RedirectToAction("Index"); // Redirect after success
                }
            }

            TempData["ErrorMessage"] = "Error updating category!";
            return RedirectToAction("Index"); // Return to index or modal if validation fails
        }



        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var newsArticles = await _newsArticleService.GetNewsArticlesAsync();
            Console.WriteLine($"Total Categories: {categories.Count()}");
            Console.WriteLine($"Total News Articles: {newsArticles.Count()}");
            var viewModel = new CategoryViewModel
            {
                Categories = categories.ToList(),
                NewsArticles = newsArticles.ToList()
            };

            return View(viewModel);
        }

    }
}
