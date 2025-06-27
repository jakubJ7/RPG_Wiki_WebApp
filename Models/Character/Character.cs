using System.ComponentModel.DataAnnotations;

namespace RPG_Wiki_WebApp.Models.Character
{
    public class Character
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Character Code.")]
        [StringLength(32, ErrorMessage = "Character code can't be longer then 32 chars.")]
        public string? CharacterCode { get; set; }

        [Required(ErrorMessage = "Please enter Name.")]
        [StringLength(50, ErrorMessage = "Name maximal lenght is 50 chars, enter shorter name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter Description.")]
        public string? Description { get; set; }

        public bool IsAccepted { get; set; }

        // klucze obce
        public int? CharacterTypeId { get; set; }
        public CharacterType? CharacterType { get; set; }
    }
}
