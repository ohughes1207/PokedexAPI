namespace PokedexAPI.Models
{
    public class PokemonVariant
    {
        public int VarId { get; set; }
        public int PokedexNum { get; set; }
        public required PokemonBase BasePokemon { get; set; }
        public required string VariantName { get; set; }
        public required string Type1 { get; set; }
        public required PokemonType Type1Rel { get; set; }
        public string? Type2 { get; set; }
        public PokemonType? Type2Rel { get; set; }
        public int TotalStats { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SPAttack { get; set; }
        public int SPDefense { get; set; }
        public int Speed { get; set; }
        public bool IsRegional { get; set; }
        public bool IsMega { get; set; }
        public required string ImgName { get; set; }
    }
}
