using System.ComponentModel.DataAnnotations;

namespace RPG_Wiki_WebApp.Models.Item
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Item Code.")]
        [StringLength(32, ErrorMessage = "Item Code can't be longer then 32 chars.")]
        public string? ItemCode { get; set; }

        [Required(ErrorMessage = "Please enter Name.")]
        [StringLength(50, ErrorMessage = "Name maximal lenght is 50 chars, enter shorter name.")]
        public string? Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be positive.")]
        public int Value { get; set; }

        // Klucze obce
        public int? ItemRarityId { get; set; }
        public ItemRarity? ItemRarity { get; set; }
    }
}
