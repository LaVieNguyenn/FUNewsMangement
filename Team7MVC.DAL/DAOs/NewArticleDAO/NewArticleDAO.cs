using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.DAOs.NewArticleDAO
{
    public class NewArticleDAO : INewArticleDAO
    {
        private readonly string _connectionString; 
        public NewArticleDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionStringDB")!;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllAsync()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM NewsArticle ORDER BY CreatedDate DESC";
                return await connection.QueryAsync<NewsArticle>(sql);
            }
        }

        public async Task<IEnumerable<NewsArticle>> GetAllNewestNews()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SSELECT * FROM NewsArticle WHERE CONVERT(Date,CreatedDate) = Convert(DATE, GETDATE()) ORDER BY CreatedDate DESC;";
                return await connection.QueryAsync<NewsArticle>(sql);
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

    }
}
