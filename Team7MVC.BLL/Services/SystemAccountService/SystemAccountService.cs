using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DTOs;
using Team7MVC.DAL.Models;
using Team7MVC.DAL.Repositories;

namespace Team7MVC.BLL.Services.SystemAccountService
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _repository;
        public SystemAccountService(ISystemAccountRepository systemAccountRepository)
        {
            _repository = systemAccountRepository;
        }
        public Task<SystemAccount> Login(string email, string password)
        {
            return _repository.Login(email, password);  
        }
        public Task<SystemAccount> GetAccountById (int accountID)
        {
            return (_repository.GetAccountById(accountID));
        }
        public Task<IEnumerable<SystemAccount>> GetAllAccounts()
        {
            return _repository.GetAllAccounts();
        }
        public async Task<bool> UpdateAccount(SystemAccountDTO model)
        {
            return await _repository.UpdateAccount(model);
        }
        public async Task<bool> DeleteAccount(int accountId)
        {
           return await _repository.DeleteAccount(accountId);
        }
        public Task<SystemAccount?> GetAccountByEmailAsync(string email) // lay tk theo email
        {
            return _repository.GetAccountByEmailAsync(email);
        }

        public Task UpdateProfileAsync(SystemAccount account) // cap nhat tk
        {
            return _repository.UpdateAccountAsync(account);
        }

        public Task<SystemAccount> GetAccountWithNewsHistoryAsync(string email)
        {
               return _repository.GetAccountWithNewsHistoryAsync(email);
        }
        public async Task<bool> AddAccount(SystemAccountDTOAdd model)
        {
            return await _repository.AddAccount(model);
        }

    }
}
        