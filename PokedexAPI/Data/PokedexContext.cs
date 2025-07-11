﻿using Microsoft.EntityFrameworkCore;
using PokedexAPI.Models;

namespace PokedexAPI.Data
{
    public class PokedexContext : DbContext
    {

        public PokedexContext(DbContextOptions<PokedexContext> options) : base(options)
        {

        }

        public DbSet<PokemonBase> Pokemons { get; set; }
        public DbSet<PokemonVariant> Variants { get; set; }
        public DbSet<PokemonType> PokemonTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonBase>()
                .HasKey(p => p.BaseId);

            modelBuilder.Entity<PokemonBase>()
                .HasMany(p => p.Variants)
                .WithOne(v => v.BasePokemon)
                .HasForeignKey(v => v.PokedexNum)
                .HasPrincipalKey(p => p.PokedexNum);

            modelBuilder.Entity<PokemonVariant>()
                .HasKey(p => p.VarId);

            // Variant -> Type1 (Many to One, foreign key: type1)
            modelBuilder.Entity<PokemonVariant>()
                .HasOne(v => v.Type1Rel)
                .WithMany()
                .HasForeignKey(v => v.Type1)
                .OnDelete(DeleteBehavior.Restrict);

            // Variant -> Type2 (Many to One, foreign key: type2, nullable)
            modelBuilder.Entity<PokemonVariant>()
                .HasOne(v => v.Type2Rel)
                .WithMany()
                .HasForeignKey(v => v.Type2)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PokemonType>()
                .HasKey(t => t.TypeName);

            // Set table names manually if you want exact matches
            //modelBuilder.Entity<PokemonBase>().ToTable("pokemon_base");
            //modelBuilder.Entity<PokemonVariant>().ToTable("variants");
            //modelBuilder.Entity<PokemonType>().ToTable("types");
        }
    }
}
