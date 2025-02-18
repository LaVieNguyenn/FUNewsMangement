using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7MVC.DAL.Models
{
    public class ReportData
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReportName { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}

