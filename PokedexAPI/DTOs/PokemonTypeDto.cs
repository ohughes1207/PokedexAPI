namespace PokedexAPI.DTOs
{
    //No Dto specialized for Response or CsvUpload since Dto would be the same in both cases
    public class PokemonTypeDto
    {
        public required string TypeName { get; set; }
        public float DefVsGrass { get; set; }
        public float DefVsFire { get; set; }
        public float DefVsWater { get; set; }
        public float DefVsBug { get; set; }
        public float DefVsNormal { get; set; }
        public float DefVsDark { get; set; }
        public float DefVsPoison { get; set; }
        public float DefVsElectric { get; set; }
        public float DefVsGround { get; set; }
        public float DefVsIce { get; set; }
        public float DefVsFairy { get; set; }
        public float DefVsSteel { get; set; }
        public float DefVsFighting { get; set; }
        public float DefVsPsychic { get; set; }
        public float DefVsRock { get; set; }
        public float DefVsGhost { get; set; }
        public float DefVsDragon { get; set; }
        public float DefVsFlying { get; set; }
    }
}
