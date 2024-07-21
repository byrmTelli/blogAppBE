namespace blogAppBE.CORE.ViewModels.UserViewModels
{
    public class AppUserViewModel: BaseViewModel
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}