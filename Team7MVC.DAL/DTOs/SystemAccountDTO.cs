using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7MVC.DAL.DTOs
{
    public class SystemAccountDTO
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountRole { get; set; }
    }
}
