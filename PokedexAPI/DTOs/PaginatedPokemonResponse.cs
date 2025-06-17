using PokedexAPI.Models;

namespace PokedexAPI.DTOs
{
    public class PaginatedPokemonResponse
    {
        public required IEnumerable<PokemonBase> Data { get; set; }
        public required int Total { get; set; }
        public required int Page { get; set; }
        public required int PerPage { get; set; }
        public required int TotalPages { get; set; }
    }
}
