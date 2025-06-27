namespace RPG_Wiki_WebApp.Models.Item.Armor
{
    public class ArmorType
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Powiązane itemki
        public ICollection<Armor>? Armors { get; set; }
    }
}
