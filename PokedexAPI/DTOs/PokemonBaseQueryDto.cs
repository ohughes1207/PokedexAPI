namespace PokedexAPI.DTOs
{
    public class PokemonBaseQueryDto
    {
        public string? SearchQuery { get; set; } = "";
        public string? T1 { get; set; } = "";
        public string? T2 { get; set; } = "";
        public int GenValue { get; set; } = 0;
        public bool Legendary { get; set; } = false;
        public bool Paradox { get; set; } = false;
        public bool Pseudo { get; set; } = false;
        public bool Ultrabeast { get; set; } = false;
        public bool Myth { get; set; } = false;
        public bool Regional { get; set; } = false;
        public bool Mega { get; set; } = false;
        public int Page { get; set; } = 1;
    }
}
