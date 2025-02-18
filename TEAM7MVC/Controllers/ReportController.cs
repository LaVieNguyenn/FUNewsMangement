using Microsoft.AspNetCore.Mvc;
using Team7MVC.BLL.Services.ReportService;
using Team7MVC.ViewModels;

namespace Team7MVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _services;

        public ReportController(IReportService reportService)
        {
            _services = reportService;
        }

        [HttpGet]
        public IActionResult GenerateReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(ReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reportData = await _services.GenerateReport(model.StartDate, model.EndDate);
                if (reportData == null)
                {
                    ModelState.AddModelError("", "No data available for the given period.");
                    return View(model);
                }

                // Chuyển đổi từ ReportData sang ReportViewModel
                var reportViewModel = new ReportViewModel
                {
                    StartDate = reportData.StartDate,
                    EndDate = reportData.EndDate,
                    ReportName = reportData.ReportName,
                    Quantity = reportData.Quantity,
                    Total = reportData.Total
                };

                return View("ReportResult", reportViewModel);
            }
            return View(model);
        }
    }
}
