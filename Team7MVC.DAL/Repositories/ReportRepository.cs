using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Team7MVC.DAL.Models;

namespace Team7MVC.DAL.Repositories.ReportRepository
{
    public class ReportRepository : IReportRepository
    {
        public async Task<ReportData> GetReportData(DateTime startDate, DateTime endDate)
        {
            
            ReportData reportData = new ReportData
            {
                StartDate = startDate,
                EndDate = endDate,
                ReportName = "Report",
                Quantity = 18,
                Total = 100.50m
            };
            return reportData;
        }
    }
}

