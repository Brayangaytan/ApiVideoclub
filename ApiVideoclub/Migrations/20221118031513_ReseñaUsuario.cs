using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiVideoclub.Migrations
{
    public partial class ReseñaUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Reseñas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reseñas_UsuarioId",
                table: "Reseñas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reseñas_AspNetUsers_UsuarioId",
                table: "Reseñas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reseñas_AspNetUsers_UsuarioId",
                table: "Reseñas");

            migrationBuilder.DropIndex(
                name: "IX_Reseñas_UsuarioId",
                table: "Reseñas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Reseñas");
        }
    }
}
