namespace PokedexAPI.Models
{
    public class PokemonBase
    {
        public int BaseId { get; set; }
        public int PokedexNum { get; set; }
        public required string BaseName { get; set; }
        public int Generation {  get; set; }
        public bool IsLegendary { get; set; }
        public bool IsParadox { get; set; }
        public bool IsPseudoLegendary { get; set; }
        public bool IsUltrabeast { get; set; }
        public bool IsMythical { get; set; }
        public required ICollection<PokemonVariant> Variants { get; set; }
    }
}
