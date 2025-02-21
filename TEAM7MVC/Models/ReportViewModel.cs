namespace Team7MVC.ViewModels
{
    public class ReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReportName { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
