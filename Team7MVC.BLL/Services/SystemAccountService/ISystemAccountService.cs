using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;
using Team7MVC.DAL.Models;

namespace Team7MVC.BLL.Services.SystemAccountService
{
    public interface ISystemAccountService
    {
        Task<SystemAccount> Login(string email, string password);
        Task<SystemAccount> GetAccountById(int accountID);
        Task<IEnumerable<SystemAccount>> GetAllAccounts();
        Task<bool> UpdateAccount(SystemAccountDTO model);
        Task<bool> DeleteAccount(int accountId);
        Task<SystemAccount?> GetAccountByEmailAsync(string email); // lay tk theo email
        Task UpdateProfileAsync(SystemAccount account); // cap nhat ho so
        Task<SystemAccount> GetAccountWithNewsHistoryAsync(string email);
        Task<bool> AddAccount(SystemAccountDTOAdd model);

    }
}
