using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsAlterConsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacaoAgendamento");

            migrationBuilder.CreateTable(
                name: "Consulta",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPaciente = table.Column<string>(type: "NVARCHAR(450)", maxLength: 450, nullable: false),
                    IdMedico = table.Column<string>(type: "NVARCHAR(450)", nullable: false),
                    IdDisponibilidade = table.Column<long>(type: "bigint", nullable: false),
                    IdStatus = table.Column<long>(type: "bigint", nullable: false),
                    Data = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consulta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consulta_AspNetUsers_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consulta_AspNetUsers_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consulta_DisponibilidadeMedico_IdDisponibilidade",
                        column: x => x.IdDisponibilidade,
                        principalTable: "DisponibilidadeMedico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consulta_Status_IdStatus",
                        column: x => x.IdStatus,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_IdDisponibilidade",
                table: "Consulta",
                column: "IdDisponibilidade");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_IdMedico",
                table: "Consulta",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_IdPaciente",
                table: "Consulta",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_IdStatus",
                table: "Consulta",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_StatusId",
                table: "Consulta",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consulta");

            migrationBuilder.CreateTable(
                name: "SolicitacaoAgendamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDisponibilidade = table.Column<long>(type: "bigint", nullable: false),
                    IdMedico = table.Column<string>(type: "NVARCHAR(450)", nullable: false),
                    IdPaciente = table.Column<string>(type: "NVARCHAR(450)", maxLength: 450, nullable: false),
                    StatusId = table.Column<long>(type: "BIGINT", nullable: true),
                    Data = table.Column<DateTime>(type: "DateTime", nullable: false),
                    IdStatus = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoAgendamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoAgendamento_AspNetUsers_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoAgendamento_AspNetUsers_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoAgendamento_DisponibilidadeMedico_IdDisponibilidade",
                        column: x => x.IdDisponibilidade,
                        principalTable: "DisponibilidadeMedico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoAgendamento_Status_IdStatus",
                        column: x => x.IdStatus,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SolicitacaoAgendamento_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoAgendamento_IdDisponibilidade",
                table: "SolicitacaoAgendamento",
                column: "IdDisponibilidade");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoAgendamento_IdMedico",
                table: "SolicitacaoAgendamento",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoAgendamento_IdPaciente",
                table: "SolicitacaoAgendamento",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoAgendamento_IdStatus",
                table: "SolicitacaoAgendamento",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoAgendamento_StatusId",
                table: "SolicitacaoAgendamento",
                column: "StatusId");
        }
    }
}
