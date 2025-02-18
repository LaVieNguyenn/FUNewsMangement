using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;

namespace Team7MVC.BLL.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoryAsync(); // Get all categories
        Task<IEnumerable<Category>> GetAllCategoryAsync(); // Get all top-level categories
        Task<IEnumerable<Category>> GetAllAsync(); // Get all categories (may include subcategories)
        Task<int> CreateCategoryAsync(Category category); // Create a new category and return the generated ID
    }
}
