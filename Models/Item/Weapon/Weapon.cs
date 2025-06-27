using System.ComponentModel.DataAnnotations;

namespace RPG_Wiki_WebApp.Models.Item.Weapon
{
    public class Weapon : Item, IValidatableObject
    {
        [Range(0, int.MaxValue, ErrorMessage = "Minimal Damage must be positive.")]
        public int MinDamage { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Maximal Damage must be positive.")]
        public int MaxDamage { get; set; }

        // Klucze obce
        public int? WeaponTypeId { get; set; }
        public WeaponType? WeaponType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinDamage > MaxDamage)
            {
                yield return new ValidationResult(
                    "Minimal Damage cannot be greater than Maximal Damage.",
                    new[] { nameof(MinDamage), nameof(MaxDamage) });
            }
        }
    }
}
