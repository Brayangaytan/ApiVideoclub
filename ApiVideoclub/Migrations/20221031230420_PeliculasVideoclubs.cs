using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiVideoclub.Migrations
{
    public partial class PeliculasVideoclubs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeliculaVideoclub",
                columns: table => new
                {
                    VideoclubId = table.Column<int>(type: "int", nullable: false),
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaVideoclub", x => new { x.PeliculaId, x.VideoclubId });
                    table.ForeignKey(
                        name: "FK_PeliculaVideoclub_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaVideoclub_Videoclubs_VideoclubId",
                        column: x => x.VideoclubId,
                        principalTable: "Videoclubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaVideoclub_VideoclubId",
                table: "PeliculaVideoclub",
                column: "VideoclubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculaVideoclub");
        }
    }
}
