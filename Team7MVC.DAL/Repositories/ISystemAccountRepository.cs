using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.Repositories
{
    public interface ISystemAccountRepository
    {
        Task<SystemAccount> Login(string email, string password);
        Task<SystemAccount> GetAccountById(int accountID);
        Task<IEnumerable<SystemAccount>> GetAllAccounts();
        Task<bool> UpdateAccount(SystemAccountDTO model);
        Task DeleteAccountById(int accountID);
        Task<SystemAccount?> GetAccountByEmailAsync(string email); // lay tk theo email
        Task UpdateAccountAsync(SystemAccount account); // cap nhat tk
        Task<SystemAccount> GetAccountWithNewsHistoryAsync(string email);
    }
}
