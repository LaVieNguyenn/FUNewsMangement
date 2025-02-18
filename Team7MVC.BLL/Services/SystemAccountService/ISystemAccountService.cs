using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;

namespace Team7MVC.BLL.Services.SystemAccountService
{
    public interface ISystemAccountService
    {
        Task<SystemAccount> Login(string email, string password);
    }
}
