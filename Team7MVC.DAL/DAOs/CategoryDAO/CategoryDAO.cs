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

    }
}
