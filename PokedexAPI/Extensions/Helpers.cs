using PokedexAPI.Models;

namespace PokedexAPI.Extensions
{
    public static class Helpers
    {
        public static Dictionary<string, float> Combine(PokemonType type1, PokemonType? type2)
        {
            var result = new Dictionary<string, float>();

            string[] types = new[]
            {
            "Normal", "Fire", "Water", "Electric", "Grass", "Ice", "Fighting", "Poison",
            "Ground", "Flying", "Psychic", "Bug", "Rock", "Ghost", "Dragon", "Dark",
            "Steel", "Fairy"
        };

            foreach (var atk in types)
            {
                float def1 = Get(type1, atk);
                float def2 = type2 != null ? Get(type2, atk) : 1f;
                result[atk] = def1 * def2;
            }

            return result;
        }

        private static float Get(PokemonType type, string attackType) => attackType switch
        {
            "Normal" => type.DefVsNormal,
            "Fire" => type.DefVsFire,
            "Water" => type.DefVsWater,
            "Electric" => type.DefVsElectric,
            "Grass" => type.DefVsGrass,
            "Ice" => type.DefVsIce,
            "Fighting" => type.DefVsFighting,
            "Poison" => type.DefVsPoison,
            "Ground" => type.DefVsGround,
            "Flying" => type.DefVsFlying,
            "Psychic" => type.DefVsPsychic,
            "Bug" => type.DefVsBug,
            "Rock" => type.DefVsRock,
            "Ghost" => type.DefVsGhost,
            "Dragon" => type.DefVsDragon,
            "Dark" => type.DefVsDark,
            "Steel" => type.DefVsSteel,
            "Fairy" => type.DefVsFairy,
            _ => 1f
        };
    }
}