using CsvHelper.Configuration;
using PokedexAPI.DTOs;

namespace PokedexAPI.Mappers
{
    public class PokemonVariantCsvDtoMap : ClassMap<PokemonVariantCsvDTO>
    {
        public PokemonVariantCsvDtoMap()
        {
            Map(m => m.PokedexNum).Name("Pokedex Number");
            Map(m => m.VariantName).Name("Variant Name");
            Map(m => m.PokemonType1).Name("Type 1");
            Map(m => m.PokemonType2).Name("Type 2");
            Map(m => m.TotalStats).Name("Total");
            Map(m => m.HP).Name("HP");
            Map(m => m.Attack).Name("Attack");
            Map(m => m.Defense).Name("Defense");
            Map(m => m.SpecialAttack).Name("Sp. Atk");
            Map(m => m.SpecialDefense).Name("Sp. Def");
            Map(m => m.Speed).Name("Speed");
            Map(m => m.Regional).Name("Regional Form");
            Map(m => m.Mega).Name("Mega");
            Map(m => m.ImageName).Name("img_name");

        }
    }
}
