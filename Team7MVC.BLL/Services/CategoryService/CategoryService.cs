using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;
using Team7MVC.DAL.Repositories;

namespace Team7MVC.BLL.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        // Constructor to inject the repository
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        // Get all top-level categories
        public Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return _repository.GetAllCategoryAsync();
        }

        // Get all categories (including subcategories)
        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return _repository.GetAllCategoriesAsync();
        }

        // Create a new category and return its CategoryID
        public async Task<int> CreateCategoryAsync(Category category)
        {
            // Call the repository to create the category and get the new CategoryID
            return await _repository.CreateCategoryAsync(category);
        }

        // Optionally implement this method if required
        public Task<IEnumerable<Category>> GetCategoryAsync()
        {
            // You can implement this if you need a method to get all categories with some specific logic
            throw new NotImplementedException();
        }
        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _repository.DeleteCategoryAsync(categoryId);
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _repository.GetCategoryByIdAsync(id); // Call the repository to get the category
        }
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            return await _repository.UpdateCategoryAsync(category);
        }


    }
}
