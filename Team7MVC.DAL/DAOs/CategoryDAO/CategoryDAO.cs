using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DAOs.NewArticleDAO;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.DAOs.CategoryDAO
{
    public class CategoryDAO : ICatogeryDAO
    {
        private readonly string _connectionString;
        public CategoryDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionStringDB")!;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM Category ORDER BY CategoryID DESC";
                return await connection.QueryAsync<Category>(sql);
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM Category WHERE ParentCategoryID IS NULL ORDER BY CategoryID DESC;";
                return await connection.QueryAsync<Category>(sql);
            }
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM Category WHERE CategoryID = @Id";
                return await connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
            }
        }

        public async Task<int> CreateCategoryAsync(Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = @"
            INSERT INTO Category (CategoryName, CategoryDescription, ParentCategoryID, IsActive)
            VALUES (@CategoryName, @CategoryDescription, @ParentCategoryId, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int);"; // Returns the new CategoryID

                var newCategoryId = await connection.ExecuteScalarAsync<int>(sql, category);
                return newCategoryId; // Return the generated ID
            }
        }
        public async Task DeleteCategoryAsync(int categoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Check if the category has any child categories (i.e., it is a parent category)
                var sqlCheckIfParent = "SELECT COUNT(*) FROM Category WHERE ParentCategoryId = @CategoryId";
                var hasChildCategories = await connection.ExecuteScalarAsync<int>(sqlCheckIfParent, new { CategoryId = categoryId });

                if (hasChildCategories > 0)
                {
                    // If the category has child categories, throw an exception
                    throw new Exception("This category cannot be deleted because it has child categories.");
                }

                // Proceed with deleting the category if it has no child categories
                var sqlDelete = "DELETE FROM Category WHERE CategoryID = @CategoryId";
                await connection.ExecuteAsync(sqlDelete, new { CategoryId = categoryId });
            }
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = @"
            UPDATE Category
            SET CategoryName = @CategoryName,
                CategoryDescription = @CategoryDescription,
                ParentCategoryId = @ParentCategoryId,
                IsActive = @IsActive
            WHERE CategoryId = @CategoryId";

                var result = await connection.ExecuteAsync(sql, category);
                return result > 0; // Return true if the update was successful
            }
        }



    }
}
