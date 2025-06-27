namespace RPG_Wiki_WebApp.Models.Item.Misc
{
    public class MiscType
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // Powiązane itemki
        public ICollection<Misc>? Miscs { get; set; }
    }
}
