using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsAgendamentoConsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoAgendamento_Convenio_Convenio",
                table: "SolicitacaoAgendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoAgendamento_Status_Status",
                table: "SolicitacaoAgendamento");

            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "Convenio");

            migrationBuilder.DropTable(
                name: "HistoricoAgendamento");

            migrationBuilder.DropTable(
                name: "Resultado");

            migrationBuilder.DropTable(
                name: "SolicitacaoExame");

            migrationBuilder.DropTable(
                name: "TiposExame");

            migrationBuilder.DropColumn(
                name: "Arquivo",
                table: "SolicitacaoAgendamento");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Status",
                newName: "DsStatus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "SolicitacaoAgendamento",
                newName: "IdStatus");

            migrationBuilder.RenameColumn(
                name: "Paciente",
                table: "SolicitacaoAgendamento",
                newName: "IdPaciente");

            migrationBuilder.RenameColumn(
                name: "Convenio",
                table: "SolicitacaoAgendamento",
                newName: "IdDisponibilidade");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitacaoAgendamento_Status",
                table: "SolicitacaoAgendamento",
                newName: "IX_SolicitacaoAgendamento_IdStatus");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitacaoAgendamento_Convenio",
                table: "SolicitacaoAgendamento",
                newName: "IX_SolicitacaoAgendamento_IdDisponibilidade");

            migrationBuilder.AddColumn<string>(
                name: "IdMedico",
                table: "SolicitacaoAgendamento",
                type: "NVARCHAR(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoAgendamento_IdMedico",
                table: "SolicitacaoAgendamento",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoAgendamento_IdPaciente",
                table: "SolicitacaoAgendamento",
                column: "IdPaciente");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoAgendamento_AspNetUsers_IdMedico",
                table: "SolicitacaoAgendamento",
                column: "IdMedico",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoAgendamento_AspNetUsers_IdPaciente",
                table: "SolicitacaoAgendamento",
                column: "IdPaciente",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoAgendamento_DisponibilidadeMedico_IdDisponibilidade",
                table: "SolicitacaoAgendamento",
                column: "IdDisponibilidade",
                principalTable: "DisponibilidadeMedico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoAgendamento_Status_IdStatus",
                table: "SolicitacaoAgendamento",
                column: "IdStatus",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoAgendamento_AspNetUsers_IdMedico",
                table: "SolicitacaoAgendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoAgendamento_AspNetUsers_IdPaciente",
                table: "SolicitacaoAgendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoAgendamento_DisponibilidadeMedico_IdDisponibilidade",
                table: "SolicitacaoAgendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoAgendamento_Status_IdStatus",
                table: "SolicitacaoAgendamento");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoAgendamento_IdMedico",
                table: "SolicitacaoAgendamento");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoAgendamento_IdPaciente",
                table: "SolicitacaoAgendamento");

            migrationBuilder.DropColumn(
                name: "IdMedico",
                table: "SolicitacaoAgendamento");

            migrationBuilder.RenameColumn(
                name: "DsStatus",
                table: "Status",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "IdStatus",
                table: "SolicitacaoAgendamento",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "IdPaciente",
                table: "SolicitacaoAgendamento",
                newName: "Paciente");

            migrationBuilder.RenameColumn(
                name: "IdDisponibilidade",
                table: "SolicitacaoAgendamento",
                newName: "Convenio");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitacaoAgendamento_IdStatus",
                table: "SolicitacaoAgendamento",
                newName: "IX_SolicitacaoAgendamento_Status");

            migrationBuilder.RenameIndex(
                name: "IX_SolicitacaoAgendamento_IdDisponibilidade",
                table: "SolicitacaoAgendamento",
                newName: "IX_SolicitacaoAgendamento_Convenio");

            migrationBuilder.AddColumn<string>(
                name: "Arquivo",
                table: "SolicitacaoAgendamento",
                type: "VARCHAR(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Solicitacao = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<long>(type: "bigint", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamento_SolicitacaoAgendamento_Solicitacao",
                        column: x => x.Solicitacao,
                        principalTable: "SolicitacaoAgendamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Status_Status",
                        column: x => x.Status,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Convenio",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Convenio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoAgendamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Solicitacao = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<long>(type: "bigint", nullable: false),
                    Atendente = table.Column<string>(type: "NVARCHAR(450)", maxLength: 450, nullable: true),
                    Data = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoAgendamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoAgendamento_SolicitacaoAgendamento_Solicitacao",
                        column: x => x.Solicitacao,
                        principalTable: "SolicitacaoAgendamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricoAgendamento_Status_Status",
                        column: x => x.Status,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resultado",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Solicitacao = table.Column<long>(type: "bigint", nullable: false),
                    Arquivo = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Atendente = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resultado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resultado_SolicitacaoAgendamento_Solicitacao",
                        column: x => x.Solicitacao,
                        principalTable: "SolicitacaoAgendamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiposExame",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposExame", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacaoExame",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdExame = table.Column<long>(type: "BIGINT", nullable: false),
                    IdSolicitacao = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoExame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoExame_SolicitacaoAgendamento_IdSolicitacao",
                        column: x => x.IdSolicitacao,
                        principalTable: "SolicitacaoAgendamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoExame_TiposExame_IdExame",
                        column: x => x.IdExame,
                        principalTable: "TiposExame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_Solicitacao",
                table: "Agendamento",
                column: "Solicitacao");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_Status",
                table: "Agendamento",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAgendamento_Solicitacao",
                table: "HistoricoAgendamento",
                column: "Solicitacao");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoAgendamento_Status",
                table: "HistoricoAgendamento",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Resultado_Solicitacao",
                table: "Resultado",
                column: "Solicitacao");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoExame_IdExame",
                table: "SolicitacaoExame",
                column: "IdExame");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoExame_IdSolicitacao",
                table: "SolicitacaoExame",
                column: "IdSolicitacao");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoAgendamento_Convenio_Convenio",
                table: "SolicitacaoAgendamento",
                column: "Convenio",
                principalTable: "Convenio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoAgendamento_Status_Status",
                table: "SolicitacaoAgendamento",
                column: "Status",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
