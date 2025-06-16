using CsvHelper.Configuration;
using PokedexAPI.DTOs;

namespace PokedexAPI.Mappers
{
    public class PokemonBaseCsvDtoMap : ClassMap<PokemonBaseCsvDto>
    {
        public PokemonBaseCsvDtoMap()
        {
            Map(m => m.PokedexNum).Name("Pokedex Number");
            Map(m => m.BaseName).Name("Base Pokemon");
            Map(m => m.Generation).Name("Generation");
            Map(m => m.Paradox).Name("Paradox");
            Map(m => m.Legendary).Name("Legendary");
            Map(m => m.PseudoLegendary).Name("Pseudo-legendary");
            Map(m => m.Ultrabeast).Name("Ultrabeast");
            Map(m => m.Mythical).Name("Mythical");
        }
    }
}
