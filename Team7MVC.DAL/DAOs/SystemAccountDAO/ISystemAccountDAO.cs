using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.DAOs.SystemAccountDAO
{
    public interface ISystemAccountDAO
    {
        Task<SystemAccount> Login(string email, string password);
    }
}
