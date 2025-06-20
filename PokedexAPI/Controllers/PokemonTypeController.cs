using CsvHelper;
using Microsoft.AspNetCore.Mvc;
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
    public class PokemonTypeController : ControllerBase
    {
        private PokedexContext _context;
        private PokemonTypeService _pokemonTypeService;

        public PokemonTypeController(PokedexContext context, PokemonTypeService pokemonTypeService)
        {
            _context = context;
            _pokemonTypeService = pokemonTypeService;
        }

        [HttpPost("upload-types")]
        public async Task<IActionResult> UploadPokemonTypes(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Context.RegisterClassMap<PokemonTypeCsvDtoMap>();
                var records = csv.GetRecords<PokemonTypeCsvDTO>().ToList();

                var newEntities = new List<PokemonType>();

                foreach (var dto in records)
                {

                    newEntities.Add(new PokemonType
                    {
                        TypeName = dto.TypeName,
                        DefVsBug = dto.DefVsBug,
                        DefVsDark = dto.DefVsDark,
                        DefVsDragon = dto.DefVsDragon,
                        DefVsElectric = dto.DefVsElectric,
                        DefVsFairy = dto.DefVsFairy,
                        DefVsFighting = dto.DefVsFighting,
                        DefVsFire = dto.DefVsFire,
                        DefVsFlying = dto.DefVsFlying,
                        DefVsGhost = dto.DefVsGhost,
                        DefVsGrass = dto.DefVsGrass,
                        DefVsGround = dto.DefVsGround,
                        DefVsIce = dto.DefVsIce,
                        DefVsNormal = dto.DefVsNormal,
                        DefVsPoison = dto.DefVsPoison,
                        DefVsPsychic = dto.DefVsPsychic,
                        DefVsRock = dto.DefVsRock,
                        DefVsSteel = dto.DefVsSteel,
                        DefVsWater = dto.DefVsWater,
                    });
                }

                await _context.PokemonTypes.AddRangeAsync(newEntities);
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
        [HttpGet("get-types")]
        public async Task<IActionResult> GetAllPokemonTypes()
        {
            var response = await _pokemonTypeService.GetAllPokemonTypes();

            return Ok(response);
        }
    }
}
