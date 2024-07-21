using System.ComponentModel.DataAnnotations;

namespace blogAppBE.CORE.RequestModels.User
{
    public class UserRegisterViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage ="Email field is required")]
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
    }
}