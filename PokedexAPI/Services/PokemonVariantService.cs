using Microsoft.EntityFrameworkCore;
using PokedexAPI.Data;
using PokedexAPI.Models;

namespace PokedexAPI.Services
{
    public class PokemonVariantService
    {
        private readonly PokedexContext _context;

        public PokemonVariantService(PokedexContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PokemonVariant>> GetAllPokemonVariants()
        {
            List<PokemonVariant> records = await _context.Variants.Include(v => v.BasePokemon).ToListAsync();
            return records;
        }
    }
}
