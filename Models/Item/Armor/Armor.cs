using System.ComponentModel.DataAnnotations;

namespace RPG_Wiki_WebApp.Models.Item.Armor
{
    public class Armor : Item
    {
        [Range(0, int.MaxValue, ErrorMessage = "Damage Reduction must be positive.")]
        public int DamageReduction { get; set; }

        // Klucze obce
        public int? ArmorTypeId { get; set; }
        public ArmorType? ArmorType { get; set; }
    }
}
