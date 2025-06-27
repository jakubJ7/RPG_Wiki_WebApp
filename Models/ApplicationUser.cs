using Microsoft.AspNetCore.Identity;

namespace RPG_Wiki_WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nickname { get; set; }
    }
}
