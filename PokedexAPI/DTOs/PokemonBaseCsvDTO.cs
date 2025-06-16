namespace PokedexAPI.DTOs
{
    public class PokemonBaseCsvDto
    {
        public int PokedexNum { get; set; }
        public required string BaseName { get; set; }
        public int Generation { get; set; }
        public bool Legendary { get; set; }
        public bool Paradox { get; set; }
        public bool PseudoLegendary { get; set; }
        public bool Ultrabeast { get; set; }
        public bool Mythical { get; set; }
    }
}
