namespace RPG_Wiki_WebApp.Models.Item.Weapon
{
    public class WeaponType
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Powiązane itemki
        public ICollection<Weapon>? Weapons { get; set; }
    }
}
