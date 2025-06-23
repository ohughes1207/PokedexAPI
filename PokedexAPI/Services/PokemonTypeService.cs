using Microsoft.EntityFrameworkCore;
using PokedexAPI.Data;
using PokedexAPI.DTOs;
using PokedexAPI.Models;

namespace PokedexAPI.Services
{
    public class PokemonTypeService
    {
        private readonly PokedexContext _context;

        public PokemonTypeService(PokedexContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PokemonTypeDto>> GetAllPokemonTypes()
        {
            List<PokemonTypeDto> records = await PokemonProjections.ToPokemonTypeDto(_context.PokemonTypes).ToListAsync();

            return records;
        }
    }
}
