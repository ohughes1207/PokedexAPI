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
    public class PokemonBasesController : ControllerBase
    {
        private readonly PokedexContext _context;
        private PokemonBaseService _pokemonBaseService;

        public PokemonBasesController(PokedexContext context, PokemonBaseService pokemonBaseService)
        {
            _context = context;
            _pokemonBaseService = pokemonBaseService;
        }
        [HttpGet("get-paginated")]
        public async Task<IActionResult> GetPaginatedBasePokemon(int pageNumber = 1)
        {
            var records = await _pokemonBaseService.GetAllPaginatedPokemon(pageNumber);

            return Ok(new
            {
                Data = records
            });

        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBasePokemon()
        {
            var records = await _pokemonBaseService.GetAllPokemon();
            return Ok(new
            {
                Data = records
            });

        }
        [HttpPost("upload-base")]
        public async Task<IActionResult> UploadPokemonBase(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Context.RegisterClassMap<PokemonBaseCsvDtoMap>();

                var records = csv.GetRecords<PokemonBaseCsvDto>().ToList();

                var existingPokedexNums = new HashSet<int>();

                var newEntities = new List<PokemonBase>();

                foreach (var dto in records)
                {
                    if (existingPokedexNums.Contains(dto.PokedexNum))
                    {
                        continue;
                    }

                    newEntities.Add(new PokemonBase
                    {
                        PokedexNum = dto.PokedexNum,
                        BaseName = dto.BaseName,
                        Generation = dto.Generation,
                        IsLegendary = dto.Legendary,
                        IsMythical = dto.Mythical,
                        IsParadox = dto.Paradox,
                        IsPseudoLegendary = dto.PseudoLegendary,
                        IsUltrabeast = dto.Ultrabeast,
                    });
                    existingPokedexNums.Add(dto.PokedexNum);

                }
                //await using var transaction = await _context.Database.BeginTransactionAsync();
                await _context.Pokemons.AddRangeAsync(newEntities);

                int created = newEntities.Count;

                await _context.SaveChangesAsync();
                //await transaction.CommitAsync();

                return Ok(new
                {
                    Created = created,
                    Total = records.Count,
                });
            }
            catch (Exception ex)
            {
                //await _context.Database.RollbackTransactionAsync();
                return StatusCode(500, $"Upload failed: {ex.Message}");
            }
        }
    }
}