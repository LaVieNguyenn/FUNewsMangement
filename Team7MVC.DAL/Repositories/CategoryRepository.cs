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
    }

}
