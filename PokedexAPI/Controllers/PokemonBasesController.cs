using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PokedexAPI.Data;
using PokedexAPI.DTOs;
using PokedexAPI.Mappers;
using PokedexAPI.Models;
using System.Globalization;
using System.Transactions;

namespace PokedexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonBasesController : ControllerBase
    {
        private readonly PokedexContext _context;

        public PokemonBasesController(PokedexContext context)
        {
            _context = context;
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