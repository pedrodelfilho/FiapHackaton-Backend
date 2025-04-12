using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaEspecialidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    IdEspecialidade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DsEspecialidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.IdEspecialidade);
                });
            migrationBuilder.InsertData(
        table: "Especialidades",
        columns: ["DsEspecialidade"],
        values: new object[,]
        {
            { "Alergologia" },
            { "Anestesiologia" },
            { "Angiologia" },
            { "Cardiologia" },
            { "Cirurgia Cardiovascular" },
            { "Cirurgia da Mão" },
            { "Cirurgia de Cabeça e Pescoço" },
            { "Cirurgia do Aparelho Digestivo" },
            { "Cirurgia Geral" },
            { "Cirurgia Oncológica" },
            { "Cirurgia Pediátrica" },
            { "Cirurgia Plástica" },
            { "Cirurgia Torácica" },
            { "Cirurgia Vascular" },
            { "Clínica Médica" },
            { "Dermatologia" },
            { "Endocrinologia e Metabologia" },
            { "Endoscopia" },
            { "Gastroenterologia" },
            { "Genética Médica" },
            { "Geriatria" },
            { "Ginecologia e Obstetrícia" },
            { "Hematologia e Hemoterapia" },
            { "Homeopatia" },
            { "Infectologia" },
            { "Mastologia" },
            { "Medicina de Família e Comunidade" },
            { "Medicina do Trabalho" },
            { "Medicina Esportiva" },
            { "Medicina Física e Reabilitação" },
            { "Medicina Intensiva" },
            { "Medicina Legal e Perícia Médica" },
            { "Medicina Nuclear" },
            { "Medicina Preventiva e Social" },
            { "Nefrologia" },
            { "Neurocirurgia" },
            { "Neurologia" },
            { "Nutrologia" },
            { "Oftalmologia" },
            { "Oncologia Clínica" },
            { "Ortopedia e Traumatologia" },
            { "Otorrinolaringologia" },
            { "Patologia" },
            { "Patologia Clínica/Medicina Laboratorial" },
            { "Pediatria" },
            { "Pneumologia" },
            { "Psiquiatria" },
            { "Radiologia e Diagnóstico por Imagem" },
            { "Radioterapia" },
            { "Reumatologia" },
            { "Urologia" }
        });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Especialidades");
        }
    }
}
