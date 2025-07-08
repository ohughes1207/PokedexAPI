using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PokedexAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    BaseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokedexNum = table.Column<int>(type: "integer", nullable: false),
                    BaseName = table.Column<string>(type: "text", nullable: false),
                    Generation = table.Column<int>(type: "integer", nullable: false),
                    IsLegendary = table.Column<bool>(type: "boolean", nullable: false),
                    IsParadox = table.Column<bool>(type: "boolean", nullable: false),
                    IsPseudoLegendary = table.Column<bool>(type: "boolean", nullable: false),
                    IsUltrabeast = table.Column<bool>(type: "boolean", nullable: false),
                    IsMythical = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.BaseId);
                    table.UniqueConstraint("AK_Pokemons_PokedexNum", x => x.PokedexNum);
                });

            migrationBuilder.CreateTable(
                name: "PokemonTypes",
                columns: table => new
                {
                    TypeName = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    DefVsGrass = table.Column<float>(type: "real", nullable: false),
                    DefVsFire = table.Column<float>(type: "real", nullable: false),
                    DefVsWater = table.Column<float>(type: "real", nullable: false),
                    DefVsBug = table.Column<float>(type: "real", nullable: false),
                    DefVsNormal = table.Column<float>(type: "real", nullable: false),
                    DefVsDark = table.Column<float>(type: "real", nullable: false),
                    DefVsPoison = table.Column<float>(type: "real", nullable: false),
                    DefVsElectric = table.Column<float>(type: "real", nullable: false),
                    DefVsGround = table.Column<float>(type: "real", nullable: false),
                    DefVsIce = table.Column<float>(type: "real", nullable: false),
                    DefVsFairy = table.Column<float>(type: "real", nullable: false),
                    DefVsSteel = table.Column<float>(type: "real", nullable: false),
                    DefVsFighting = table.Column<float>(type: "real", nullable: false),
                    DefVsPsychic = table.Column<float>(type: "real", nullable: false),
                    DefVsRock = table.Column<float>(type: "real", nullable: false),
                    DefVsGhost = table.Column<float>(type: "real", nullable: false),
                    DefVsDragon = table.Column<float>(type: "real", nullable: false),
                    DefVsFlying = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes", x => x.TypeName);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    VarId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PokedexNum = table.Column<int>(type: "integer", nullable: false),
                    VariantName = table.Column<string>(type: "text", nullable: false),
                    Type1 = table.Column<string>(type: "text", nullable: false),
                    Type2 = table.Column<string>(type: "text", nullable: true),
                    TotalStats = table.Column<int>(type: "integer", nullable: false),
                    HP = table.Column<int>(type: "integer", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    Defense = table.Column<int>(type: "integer", nullable: false),
                    SPAttack = table.Column<int>(type: "integer", nullable: false),
                    SPDefense = table.Column<int>(type: "integer", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false),
                    IsRegional = table.Column<bool>(type: "boolean", nullable: false),
                    IsMega = table.Column<bool>(type: "boolean", nullable: false),
                    ImgName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.VarId);
                    table.ForeignKey(
                        name: "FK_Variants_PokemonTypes_Type1",
                        column: x => x.Type1,
                        principalTable: "PokemonTypes",
                        principalColumn: "TypeName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Variants_PokemonTypes_Type2",
                        column: x => x.Type2,
                        principalTable: "PokemonTypes",
                        principalColumn: "TypeName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Variants_Pokemons_PokedexNum",
                        column: x => x.PokedexNum,
                        principalTable: "Pokemons",
                        principalColumn: "PokedexNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Variants_PokedexNum",
                table: "Variants",
                column: "PokedexNum");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_Type1",
                table: "Variants",
                column: "Type1");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_Type2",
                table: "Variants",
                column: "Type2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "PokemonTypes");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
