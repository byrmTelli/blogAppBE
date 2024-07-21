using Microsoft.AspNetCore.Identity;

namespace blogAppBE.CORE.DBModels
{
    public class AppUser:IdentityUser
    {
        public DateTime CreatedDate { get; set; }
    }
}