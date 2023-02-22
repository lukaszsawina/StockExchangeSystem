namespace WebApp.Models
{
    public class AdminViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<AppUserModel> Users { get; set; }
    }
}
