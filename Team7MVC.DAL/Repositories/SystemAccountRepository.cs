using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.DAOs.SystemAccountDAO;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private readonly ISystemAccountDAO _systemAccountDAO;
        public SystemAccountRepository(ISystemAccountDAO systemAccountDAO)
        {
            _systemAccountDAO = systemAccountDAO;
        }
        public async Task<SystemAccount> Login(string email, string password)
        {
            return  await _systemAccountDAO.Login(email, password);   
        }
    }
}
