using Microsoft.EntityFrameworkCore;
using PokedexAPI.Data;
using PokedexAPI.DTOs;
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
            List<PokemonVariant> variants = await _context.Variants.Include(v => v.BasePokemon).ToListAsync();
            return variants;
        }
        public async Task<IEnumerable<PokemonVariantSearchResponseDto>> GetVariantBySearch(string searchQuery)
        {
            //List<PokemonVariant> records = await _context.Variants.Where(v => EF.Functions.ILike(v.VariantName, searchQuery)).Include(v => v.Type1).Include(v => v.Type2).ToListAsync();
            
            List<PokemonVariantSearchResponseDto> variants = await PokemonProjections.ToPokemonVariantSearchResponseDto(_context.Variants.Where(v => v.VariantName.ToLower().Contains(searchQuery.ToLower()))
                .Include(v => v.Type1Rel)
                .Include(v => v.Type2Rel))
                .ToListAsync();

            return variants;
        }
    }
}
