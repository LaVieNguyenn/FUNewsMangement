using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;
using Team7MVC.DAL.Repositories;

using System;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;

namespace Team7MVC.BLL.Services.ReportService
{
    public interface IReportService
    {
        Task<ReportData> GenerateReport(DateTime startDate, DateTime endDate);
    }
}
