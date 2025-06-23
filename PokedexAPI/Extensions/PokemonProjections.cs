using Microsoft.EntityFrameworkCore;
using PokedexAPI.DTOs;
using PokedexAPI.Extensions;
using PokedexAPI.Models;

public static class PokemonProjections
{
    public static IQueryable<PokemonBaseResponseDto> ToPokemonBaseResponseDto(this IQueryable<PokemonBase> query)
    {
        return query.Select(p => new PokemonBaseResponseDto
        {
            PokedexNum = p.PokedexNum,
            BaseId = p.BaseId,
            BaseName = p.BaseName,
            Generation = p.Generation,
            IsLegendary = p.IsLegendary,
            IsMythical = p.IsMythical,
            IsParadox = p.IsParadox,
            IsPseudoLegendary = p.IsPseudoLegendary,
            IsUltrabeast = p.IsUltrabeast,
            Variants = p.Variants.Select(v => new PokemonVariantResponseDto
            {
                PokedexNum = v.PokedexNum,
                VarId = v.VarId,
                VariantName = v.VariantName,
                PokemonType1 = v.Type1,
                PokemonType2 = v.Type2,
                Attack = v.Attack,
                Defense = v.Defense,
                SpecialAttack = v.SPAttack,
                SpecialDefense = v.SPDefense,
                Speed = v.Speed,
                TotalStats = v.TotalStats,
                HP = v.HP,
                IsMega = v.IsMega,
                IsRegional = v.IsRegional,
                ImageName = v.ImgName,
            }).ToList()
        });
    }
    public static IQueryable<PokemonVariantSearchResponseDto> ToPokemonVariantSearchResponseDto(this IQueryable<PokemonVariant> query)
    {
        return query.Select(v => new PokemonVariantSearchResponseDto
        {
            PokedexNum = v.PokedexNum,
            VariantName = v.VariantName,
            PokemonType1 = v.Type1,
            PokemonType2 = v.Type2,
            Attack = v.Attack,
            Defense = v.Defense,
            SpecialAttack = v.SPAttack,
            SpecialDefense = v.SPDefense,
            Speed = v.Speed,
            HP = v.HP,
            IsMega = v.IsMega,
            IsRegional = v.IsRegional,
            TotalStats= v.TotalStats,
            CombinedDefenses = Helpers.Combine(v.Type1Rel, v.Type2Rel),
        });
    }
    public static IQueryable<PokemonTypeDto> ToPokemonTypeDto(this IQueryable<PokemonType> query)
    {
        return query.Select(t => new PokemonTypeDto
        {
            TypeName = t.TypeName,
            DefVsBug = t.DefVsBug,
            DefVsDark = t.DefVsDark,
            DefVsDragon = t.DefVsDragon,
            DefVsElectric = t.DefVsElectric,
            DefVsFairy = t.DefVsFairy,
            DefVsFighting = t.DefVsFighting,
            DefVsFire = t.DefVsFire,
            DefVsFlying = t.DefVsFlying,
            DefVsGhost = t.DefVsGhost,
            DefVsGrass = t.DefVsGrass,
            DefVsGround = t.DefVsGround,
            DefVsIce = t.DefVsIce,
            DefVsNormal = t.DefVsNormal,
            DefVsPoison = t.DefVsPoison,
            DefVsPsychic = t.DefVsPsychic,
            DefVsRock = t.DefVsRock,
            DefVsSteel = t.DefVsSteel,
            DefVsWater = t.DefVsWater,
        });
    }
}