﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;
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
        public async Task<IEnumerable<SystemAccount>> GetAllAccounts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT AccountID, AccountName, AccountEmail, AccountRole FROM SystemAccount";
                return await connection.QueryAsync<SystemAccount>(sql);
            }
        }
        public async Task<SystemAccount> GetAccountById(int accountID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT AccountID, AccountName, AccountEmail, AccountRole FROM SystemAccount WHERE AccountID = @AccountID";
                return await connection.QueryFirstOrDefaultAsync<SystemAccount>(sql, new { AccountID = accountID });
            }
        }
        public async Task<bool> UpdateAccount(SystemAccountDTO model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"UPDATE SystemAccount 
                    SET AccountName = @AccountName, 
                        AccountEmail = @AccountEmail, 
                        AccountRole = @AccountRole 
                    WHERE AccountID = @AccountID";

                var affectedRows = await connection.ExecuteAsync(sql, new
                {
                    model.AccountID,
                    model.AccountName,
                    model.AccountEmail,
                    model.AccountRole
                });

                return affectedRows > 0;
            }
        }
        public async Task<bool> AddAccount(SystemAccountDTOAdd model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"INSERT INTO SystemAccount (AccountName, AccountEmail, AccountRole, AccountPassword) 
                    VALUES (@AccountName, @AccountEmail, @AccountRole, @AccountPassword)";

                var affectedRows = await connection.ExecuteAsync(sql, new
                {
                    model.AccountName,
                    model.AccountEmail,
                    model.AccountRole,
                    model.AccountPassword
                });

                return affectedRows > 0;
            }
        }
        public async Task<bool> DeleteAccount(int accountId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var deleteRelatedData = @"
                DELETE FROM Tag WHERE AccountID = @AccountID;
                DELETE FROM Category WHERE AccountID = @AccountID;
                DELETE FROM NewsArticle WHERE AccountID = @AccountID;
                DELETE FROM NewsTag WHERE AccountID = @AccountID;
            ";
                    await connection.ExecuteAsync(deleteRelatedData, new { AccountID = accountId });

                    var deleteAccount = "DELETE FROM SystemAccount WHERE AccountID = @AccountID";
                    var affectedRows = await connection.ExecuteAsync(deleteAccount, new { AccountID = accountId });

                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa: " + ex.Message);
                return false;
            }
        }


    }
}
