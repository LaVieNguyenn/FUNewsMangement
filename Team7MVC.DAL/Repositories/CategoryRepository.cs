using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DAOs.CategoryDAO;
using Team7MVC.DAL.DAOs.NewArticleDAO;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICatogeryDAO _CatogeryDAO;

        public CategoryRepository(ICatogeryDAO CatogeryDAO)
        {
            _CatogeryDAO = CatogeryDAO;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _CatogeryDAO.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _CatogeryDAO.GetAllCategory();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _CatogeryDAO.GetByIdAsync(id);
        }

        public async Task<int> CreateCategoryAsync(Category category)
        {
            // Call the DAO layer to insert the category and get the new CategoryID
            return await _CatogeryDAO.CreateCategoryAsync(category);
        }
        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _CatogeryDAO.DeleteCategoryAsync(categoryId);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            // Retrieve the category to be updated
            var existingCategory = await _CatogeryDAO.GetByIdAsync(category.CategoryId);
            if (existingCategory == null)
            {
                // If the category doesn't exist, return false
                Console.WriteLine("Cant find CategoryID!");
                return false;
            }

            // Update the properties of the existing category
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.CategoryDescription = category.CategoryDescription;
            existingCategory.ParentCategoryId = category.ParentCategoryId;
            existingCategory.IsActive = category.IsActive;

            // Save changes to the database by passing the updated category
            await _CatogeryDAO.UpdateCategoryAsync(existingCategory);

            // Return true if the category was successfully updated
            return true;
        }


    }

}
