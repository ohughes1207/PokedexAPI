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
        public async Task<PaginatedPokemonResponse> GetAllPaginatedPokemon(int page)
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

            var response = new PaginatedPokemonResponse
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