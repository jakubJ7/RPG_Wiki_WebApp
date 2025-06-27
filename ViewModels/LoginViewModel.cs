using System.ComponentModel.DataAnnotations;

namespace RPG_Wiki_WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
