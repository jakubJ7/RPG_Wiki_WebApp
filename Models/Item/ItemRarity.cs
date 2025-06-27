namespace RPG_Wiki_WebApp.Models.Item
{
    public class ItemRarity
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Powiązane itemki
        public ICollection<Item>? Items { get; set; }
    }
}
