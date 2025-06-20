using Microsoft.EntityFrameworkCore;
using PokedexAPI.Data;
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
        public async Task<IEnumerable<PokemonType>> GetAllPokemonTypes()
        {
            List<PokemonType> records = await _context.PokemonTypes.ToListAsync();
            return records;
        }
    }
}
