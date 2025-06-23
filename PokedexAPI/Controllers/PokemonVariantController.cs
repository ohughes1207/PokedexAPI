using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokedexAPI.Data;
using PokedexAPI.DTOs;
using PokedexAPI.Mappers;
using PokedexAPI.Models;
using PokedexAPI.Services;
using System.Globalization;

namespace PokedexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonVariantController : ControllerBase
    {
        private readonly PokedexContext _context;
        private PokemonVariantService _pokemonVariantsService;

        public PokemonVariantController(PokedexContext context, PokemonVariantService pokemonVariantsService)
        {
            _context = context;
            _pokemonVariantsService = pokemonVariantsService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPokemonVariants(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Context.RegisterClassMap<PokemonVariantCsvDtoMap>();
                var records = csv.GetRecords<CsvUploadPokemonVariantDto>().ToList();

                // Load related entities into memory
                var pokemonLookup = await _context.Pokemons
                    .ToDictionaryAsync(p => p.PokedexNum);

                var typeLookup = await _context.PokemonTypes
                    .ToDictionaryAsync(t => t.TypeName);

                var newEntities = new List<PokemonVariant>();

                foreach (var dto in records)
                {
                    if (!pokemonLookup.TryGetValue(dto.PokedexNum, out var basePokemon))
                        continue;

                    if (!typeLookup.TryGetValue(dto.PokemonType1, out var type1))
                        continue;

                    PokemonType? type2 = null;
                    if (!string.IsNullOrEmpty(dto.PokemonType2))
                    {
                        typeLookup.TryGetValue(dto.PokemonType2, out type2);
                        if (type2 == null)
                            continue;
                    }

                    newEntities.Add(new PokemonVariant
                    {
                        PokedexNum = dto.PokedexNum,
                        BasePokemon = basePokemon,
                        VariantName = dto.VariantName,
                        Type1 = dto.PokemonType1,
                        Type1Rel = type1,
                        Type2 = string.IsNullOrWhiteSpace(dto.PokemonType2) ? null : dto.PokemonType2,
                        Type2Rel = type2,
                        TotalStats = dto.TotalStats,
                        HP = dto.HP,
                        Attack = dto.Attack,
                        Defense = dto.Defense,
                        SPAttack = dto.SpecialAttack,
                        SPDefense = dto.SpecialDefense,
                        Speed = dto.Speed,
                        IsRegional = dto.Regional,
                        IsMega = dto.Mega,
                        ImgName = dto.ImageName
                    });
                }

                await _context.Variants.AddRangeAsync(newEntities);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Created = newEntities.Count,
                    Total = records.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Upload failed: {ex.Message}");
            }
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllPokemonVariants()
        {
            var response = await _pokemonVariantsService.GetAllPokemonVariants();

            return Ok(response);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetVariantBySearch(string searchQuery)
        {
            var response = await _pokemonVariantsService.GetVariantBySearch(searchQuery);

            return Ok(response);
        }
    }
}