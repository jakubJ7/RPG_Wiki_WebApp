using System.ComponentModel.DataAnnotations;

namespace RPG_Wiki_WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny format adresu e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pseudonim w grze jest wymagany")]
        public string Nickname { get; set; }
    }
}
