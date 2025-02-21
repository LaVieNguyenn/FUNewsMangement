using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team7MVC.DAL.Models;

using System;
using System.Threading.Tasks;
using Team7MVC.DAL.Repositories.ReportRepository;
using Team7MVC.DAL.Models;

namespace Team7MVC.BLL.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IReportRepository reportRepository)
        {
            _repository = reportRepository;
        }

        public Task<ReportData> GenerateReport(DateTime startDate, DateTime endDate)
        {
            return _repository.GetReportData(startDate, endDate);
        }
    }
}

