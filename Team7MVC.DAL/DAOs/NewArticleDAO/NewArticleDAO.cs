using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;
using Team7MVC.DAL.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Team7MVC.DAL.DAOs.NewArticleDAO
{
    public class NewArticleDAO : INewArticleDAO
    {
        private readonly string _connectionString;
        public NewArticleDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionStringDB")!;
        }

        public async Task<IEnumerable<NewsArticleDTO>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT na.NewsArticleID, na.NewsTitle, na.Headline, na.CreatedDate, na.NewsContent, na.NewsSource, c.CategoryName, sa.AccountName FROM NewsArticle na JOIN Category c ON na.CategoryID = c.CategoryID JOIN SystemAccount sa ON na.CreatedByID = sa.AccountID ORDER BY na.CreatedDate DESC";
                return await connection.QueryAsync<NewsArticleDTO>(sql);
            }
        }

        public async Task<IEnumerable<NewsArticle>> GetAllNewestNews()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM NewsArticle WHERE CONVERT(Date,CreatedDate) = Convert(DATE, GETDATE()) ORDER BY CreatedDate DESC;";
                return await connection.QueryAsync<NewsArticle>(sql);
            }
        }

        public async Task<IEnumerable<NewsArticleDTO>> GetAllNewstNewByCategory(string CategoryName, int max)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT TOP(@Max) na.NewsArticleID, na.NewsTitle, na.Headline, na.CreatedDate, na.NewsContent, na.NewsSource, c.CategoryName, sa.AccountName FROM NewsArticle na JOIN Category c ON na.CategoryID = c.CategoryID JOIN SystemAccount sa ON na.CreatedByID = sa.AccountID WHERE c.CategoryName=@CategoryName ORDER BY na.CreatedDate DESC";
                return await connection.QueryAsync<NewsArticleDTO>(sql, new { Max = max, CategoryName = CategoryName });
            }
        }

        public async Task<NewsArticle> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM NewsArticle WHERE NewsArticleID = @Id";
                return await connection.QueryFirstOrDefaultAsync<NewsArticle>(sql, new { Id = id });
            }
        }
        public async Task<IEnumerable<NewsArticle>> GetAllNews()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM NewsArticle ORDER BY NewsArticleID DESC";
                return await connection.QueryAsync<NewsArticle>(sql);
            }
        }
    }
}