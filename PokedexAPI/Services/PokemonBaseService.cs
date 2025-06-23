using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using PokedexAPI.Data;
using PokedexAPI.DTOs;
using PokedexAPI.Models;
using System.Collections;
using System.ComponentModel;

namespace PokedexAPI.Services
{
    public class PokemonBaseService
    {
        private readonly PokedexContext _context;

        public PokemonBaseService(PokedexContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PokemonBase>> GetAllPokemon()
        {
            List<PokemonBase> records = await _context.Pokemons.ToListAsync();
            return records;
        }
        public async Task<IEnumerable<PokemonBase>> GetPokemonByName(string query)
        {
            List<PokemonBase> records = await _context.Pokemons.Where(p => p.BaseName.ToLower().Contains(query.ToLower())).ToListAsync();

            return records;
        }
        public async Task<PaginatedPokemonResponseDto> GetAllPaginatedPokemon(int page)
        {
            int limit = 50;
            int count = await _context.Pokemons.CountAsync();
            int skip = (page - 1) * limit;

            int total_pages;

            if (count % limit ==0)
            {
                total_pages = count / limit;
            }
            else
            {
                total_pages = (count / limit)+1;
            }

            var records = await _context.Pokemons.Skip(skip).Take(limit).ToListAsync();

            var response = new PaginatedPokemonResponseDto
            {
                Data = records,
                Total = count,
                Page = page,
                PerPage = limit,
                TotalPages = total_pages
            };

            return response;
        }

        public async Task<PaginatedPokemonResponseDto> GetPokemonByFilter(string searchQuery, string T1, string T2, int genValue, bool Legendary, bool Paradox, bool Pseudo, bool Ultrabeast, bool Myth, bool Regional, bool Mega, int page)
        {
            var query = _context.Pokemons.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                //For when using in memory database
                query = query.Where(p => p.BaseName.ToLower().Contains(searchQuery));
                
                //ILIKE only works with PostgreSQL
                //query = query.Where(p => EF.Functions.ILike(p.BaseName, searchQuery));
            }
            if (Legendary)
            {
                query = query.Where(p => p.IsLegendary == true);
            }
            if (genValue!=0)
            {
                query = query.Where(p => p.Generation == genValue);
            }
            if (Paradox)
            {
                query = query.Where(p => p.IsParadox == true);
            }
            if (Pseudo)
            {
                query = query.Where(p => p.IsPseudoLegendary == true);
            }
            if (Ultrabeast)
            {
                query = query.Where(p => p.IsUltrabeast == true);
            }
            if (Myth)
            {
                query = query.Where(p => p.IsMythical == true);
            }
            if (!string.IsNullOrEmpty(T1))
            {
                query = query.Where(p => p.Variants.Any(v => v.Type1Rel.TypeName == T1 || (v.Type2Rel == null || v.Type2Rel.TypeName == T1)));
            }
            if (!string.IsNullOrEmpty(T2))
            {
                if (T2==T1)
                {
                    query = query.Where(p => p.Variants.Any(v => v.Type1Rel.TypeName == T1 && (v.Type2Rel == null || string.IsNullOrEmpty(v.Type2Rel.TypeName))));
                }
                else if (!string.IsNullOrEmpty(T1))
                {
                    query = query.Where(p => p.Variants.Any(v => v.Type1Rel.TypeName == T1 && (v.Type2Rel == null || v.Type2Rel.TypeName==T2)) || p.Variants.Any(v => v.Type1Rel.TypeName == T2 && (v.Type2Rel == null || v.Type2Rel.TypeName == T1)));
                }
                else
                {
                    query = query.Where(p => p.Variants.Any(v => v.Type1Rel.TypeName == T2 || (v.Type2Rel == null || v.Type2Rel.TypeName == T2)));
                }
            }
            if (Regional)
            {
                query = query.Where(p => p.Variants.Any(v => v.IsRegional==true));
            }
            if (Mega)
            {
                query = query.Where(p => p.Variants.Any(v => v.IsMega == true));
            }

            int limit = 50;
            int count = await query.CountAsync();
            int skip = (page - 1) * limit;

            int total_pages;

            if (count % limit == 0)
            {
                total_pages = count / limit;
            }
            else
            {
                total_pages = (count / limit) + 1;
            }

            var records = await query.Skip(skip).Take(limit).ToListAsync();

            var response = new PaginatedPokemonResponseDto
            {
                Data = records,
                Total = count,
                Page = page,
                PerPage = limit,
                TotalPages = total_pages
            };

            return response;
        }
    }
}