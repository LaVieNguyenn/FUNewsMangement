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
            using(var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM SystemAccount WHERE AccountEmail = @Email AND AccountPassword = @Password";
                return connection.QueryFirstOrDefault<SystemAccount>(sql, new {Email = email, Password = password});
            }
        }
    }
}
