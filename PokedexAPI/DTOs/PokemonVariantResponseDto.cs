namespace PokedexAPI.DTOs
{
    public class PokemonVariantResponseDto
    {
        public int PokedexNum { get; set; }
        public required string VariantName { get; set; }
        public required string PokemonType1 { get; set; }
        public string? PokemonType2 { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int HP { get; set; }
        public int TotalStats { get; set; }
        public bool IsRegional { get; set; }
        public bool IsMega { get; set; }
        public required string ImageName { get; set; }
    }
}
