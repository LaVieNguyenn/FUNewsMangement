using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.DAOs.CategoryDAO
{
    public interface ICatogeryDAO
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategory();

        Task<int> CreateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }

}
