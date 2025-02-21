using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.DAOs.SystemAccountDAO
{
    public class SystemAccountDAO : ISystemAccountDAO
    {
        private readonly string _connectionString;
        public SystemAccountDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionStringDB")!;
        }
        public async Task<SystemAccount> Login(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM SystemAccount WHERE AccountEmail = @Email AND AccountPassword = @Password";
                return connection.QueryFirstOrDefault<SystemAccount>(sql, new { Email = email, Password = password });
            }
        }

        // lay tk theo email
        public async Task<SystemAccount?> GetAccountByEmailAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM SystemAccount WHERE AccountEmail = @Email";
                return await connection.QueryFirstOrDefaultAsync<SystemAccount>(sql, new { Email = email });
            }
        }

        // cap nhat tk
        public async Task UpdateAccountAsync(SystemAccount account)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = @"UPDATE SystemAccount 
                            SET AccountName = @AccountName
                            WHERE AccountEmail = @AccountEmail";
                await connection.ExecuteAsync(sql, new { account.AccountName, account.AccountEmail });
            }
        }
        public async Task<SystemAccount?> GetAccountWithNewsHistoryAsync(string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"SELECT a.*, n.NewsArticleID, n.NewsTitle, n.Headline, n.CreatedDate, 
                                   n.NewsContent, n.NewsSource, n.CategoryID, n.NewsStatus 
                            FROM SystemAccount a
                            LEFT JOIN NewsArticle n ON a.AccountID = n.CreatedByID
                            WHERE a.AccountEmail = @Email";

                var accountDictionary = new Dictionary<int, SystemAccount>();

                var result = await connection.QueryAsync<SystemAccount, NewsArticle, SystemAccount>(
                    sql,
                    (account, newsArticle) =>
                    {
                        if (!accountDictionary.TryGetValue(account.AccountId, out var existingAccount))
                        {
                            existingAccount = account;
                            existingAccount.NewsArticleCreatedBies = new List<NewsArticle>();
                            accountDictionary.Add(existingAccount.AccountId, existingAccount);
                        }

                        if (newsArticle != null)
                        {
                            existingAccount.NewsArticleCreatedBies.Add(newsArticle);
                        }

                        return existingAccount;
                    },
                    new { Email = email },
                    splitOn: "NewsArticleID"
                );

                return result.FirstOrDefault();
            }
        }
    }
}
