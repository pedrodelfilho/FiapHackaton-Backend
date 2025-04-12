using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarFkEspecialidadeEmUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DsEspecialidade",
                table: "Especialidades",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "IdEspecialidade",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdEspecialidade",
                table: "AspNetUsers",
                column: "IdEspecialidade");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Especialidades_IdEspecialidade",
                table: "AspNetUsers",
                column: "IdEspecialidade",
                principalTable: "Especialidades",
                principalColumn: "IdEspecialidade",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Especialidades_IdEspecialidade",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdEspecialidade",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdEspecialidade",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "DsEspecialidade",
                table: "Especialidades",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);
        }
    }
}
