using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Task<SystemAccount?> GetAccountByEmailAsync(string email) // lay tk theo email
        {
            return _repository.GetAccountByEmailAsync(email);
        }

        public Task UpdateProfileAsync(SystemAccount account) // cap nhat tk
        {
            return _repository.UpdateAccountAsync(account);
        }
    }
}
        