namespace PokedexAPI.DTOs
{
    public class CsvUploadPokemonVariantDto
    {
        public int PokedexNum { get; set; }
        public required string VariantName { get; set; }
        public required string PokemonType1 { get; set; }
        public string? PokemonType2 { get; set; } = null;
        public int TotalStats { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public bool Regional { get; set; }
        public bool Mega { get; set; }
        public required string ImageName { get; set; }
    }
}
