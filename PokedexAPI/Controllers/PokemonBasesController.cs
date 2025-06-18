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

        [HttpGet("get-filtered")]
        public async Task<IActionResult> GetFilteredPokemon(
            string searchQuery = "",
            string T1 = "",
            string T2 = "",
            bool Legendary = false,
            int genValue = 0,
            bool Paradox = false,
            bool Pseudo = false,
            bool Ultrabeast = false,
            bool Myth = false,
            bool Regional = false,
            bool Mega=false,
            int page=1)
        {
            PaginatedPokemonResponse response = await _pokemonBaseService.GetPokemonByFilter(searchQuery.ToLower(), T1, T2, genValue, Legendary, Paradox, Pseudo, Ultrabeast, Myth, Regional, Mega, page);


            if (response.Data.Count() == 0)
            {
                return BadRequest(new
                {
                    message = "Pokemon not found!"
                });
            }
            else
            {
                return Ok(new
                {
                    data = response.Data,
                    total = response.Total,
                    page = response.Page,
                    perPage = response.PerPage,
                    totalPages = response.TotalPages
                });
            }

        }
        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetPaginatedBasePokemon(string searchQuery = "")
        {
            IEnumerable<PokemonBase> response = await _pokemonBaseService.GetPokemonByName(searchQuery);


            if (response.Count() == 0)
            {
                return BadRequest(new
                {
                    message = "Pokemon not found!"
                });
            }
            else
            {
                return Ok(new
                {
                    data = response
                });
            }

        }
        [HttpGet("get-paginated")]
        public async Task<IActionResult> GetPaginatedBasePokemon(int pageNumber = 1)
        {
            PaginatedPokemonResponse response = await _pokemonBaseService.GetAllPaginatedPokemon(pageNumber);

            return Ok(response);

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