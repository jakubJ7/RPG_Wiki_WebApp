namespace RPG_Wiki_WebApp.Models.Item.Misc
{
    public class Misc : Item
    {
        // Klucze obce
        public int? MiscTypeId { get; set; }
        public MiscType? MiscType { get; set; }
    }
}
