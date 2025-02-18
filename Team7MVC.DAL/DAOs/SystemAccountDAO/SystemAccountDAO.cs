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
    }
}
