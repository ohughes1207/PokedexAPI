using CsvHelper.Configuration;
using PokedexAPI.DTOs;

namespace PokedexAPI.Mappers
{
    public class PokemonTypeCsvDtoMap : ClassMap<PokemonTypeCsvDTO>
    {
        public PokemonTypeCsvDtoMap()
        {
            Map(m => m.TypeName).Name("type_name");
            Map(m => m.DefVsGrass).Name("def_vs_Grass");
            Map(m => m.DefVsFire).Name("def_vs_Fire");
            Map(m => m.DefVsWater).Name("def_vs_Water");
            Map(m => m.DefVsBug).Name("def_vs_Bug");
            Map(m => m.DefVsNormal).Name("def_vs_Normal");
            Map(m => m.DefVsDark).Name("def_vs_Dark");
            Map(m => m.DefVsPoison).Name("def_vs_Poison");
            Map(m => m.DefVsElectric).Name("def_vs_Electric");
            Map(m => m.DefVsGround).Name("def_vs_Ground");
            Map(m => m.DefVsIce).Name("def_vs_Ice");
            Map(m => m.DefVsFairy).Name("def_vs_Fairy");
            Map(m => m.DefVsSteel).Name("def_vs_Steel");
            Map(m => m.DefVsFighting).Name("def_vs_Fighting");
            Map(m => m.DefVsPsychic).Name("def_vs_Psychic");
            Map(m => m.DefVsRock).Name("def_vs_Rock");
            Map(m => m.DefVsGhost).Name("def_vs_Ghost");
            Map(m => m.DefVsDragon).Name("def_vs_Dragon");
            Map(m => m.DefVsFlying).Name("def_vs_Flying");
        }
    }
}
