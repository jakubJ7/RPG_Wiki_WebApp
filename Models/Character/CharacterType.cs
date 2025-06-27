namespace RPG_Wiki_WebApp.Models.Character
{
    public class CharacterType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsAccepted { get; set; }

        // powiązane postacie
        public ICollection<Character>? Characters { get; set; }
    }
}
