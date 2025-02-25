namespace Team7MVC.Models
{
    public class ManageAccountModel
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountRole { get; set; }

    }
    public class SystemAccountDTONew
    {
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountRole { get; set; }
        public string AccountPassword { get; set; }
    }
}
