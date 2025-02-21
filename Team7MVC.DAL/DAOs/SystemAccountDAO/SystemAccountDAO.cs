using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
        public async Task DeleteAccountById(int accountID)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var sql = "DELETE FROM SystemAccount WHERE AccountID = @AccountID";

                    await connection.ExecuteAsync(sql, new { AccountID = accountID });

                    Console.WriteLine($"Account with ID {accountID} deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting account: {ex.Message}");
            }
        }
    }
    }
