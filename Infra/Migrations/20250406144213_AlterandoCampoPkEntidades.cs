using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoCampoPkEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Especialidades_IdEspecialidade",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades");

            migrationBuilder.DropColumn(
                name: "IdEspecialidade",
                table: "Especialidades");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Especialidades",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "IdEspecialidade",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Especialidades_IdEspecialidade",
                table: "AspNetUsers",
                column: "IdEspecialidade",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Especialidades_IdEspecialidade",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Especialidades");

            migrationBuilder.AddColumn<int>(
                name: "IdEspecialidade",
                table: "Especialidades",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "IdEspecialidade",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Especialidades",
                table: "Especialidades",
                column: "IdEspecialidade");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Especialidades_IdEspecialidade",
                table: "AspNetUsers",
                column: "IdEspecialidade",
                principalTable: "Especialidades",
                principalColumn: "IdEspecialidade",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
