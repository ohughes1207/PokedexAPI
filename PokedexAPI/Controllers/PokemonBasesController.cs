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
    public class PokemonBaseController : ControllerBase
    {
        private readonly PokedexContext _context;
        private PokemonBaseService _pokemonBaseService;

        public PokemonBaseController(PokedexContext context, PokemonBaseService pokemonBaseService)
        {
            _context = context;
            _pokemonBaseService = pokemonBaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilteredPokemon([FromQuery] PokemonBaseQueryDto query)
        {
            query.SearchQuery ??= "";
            query.T1 ??= "";
            query.T2 ??= "";

            PaginatedPokemonResponseDto response = await _pokemonBaseService.GetPokemonByFilter(query.SearchQuery.ToLower(), query.T1, query.T2, query.GenValue, query.Legendary, query.Paradox, query.Pseudo, query.Ultrabeast, query.Myth, query.Regional, query.Mega, query.Page);

            return Ok(new
            {
                data = response.Data,
                total = response.Total,
                page = response.Page,
                perPage = response.PerPage,
                totalPages = response.TotalPages
            });

        }
        [HttpPost]
        public async Task<IActionResult> UploadPokemonBase(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Context.RegisterClassMap<PokemonBaseCsvDtoMap>();

                var records = csv.GetRecords<CsvUploadPokemonBaseDto>().ToList();

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