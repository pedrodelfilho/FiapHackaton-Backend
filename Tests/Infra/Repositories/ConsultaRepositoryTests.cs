using Domain.Entities;
using Infra.Data.Context;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infra.Repositories
{
    public class ConsultaRepositoryTests
    {
        private readonly DataContext _context;
        private readonly ConsultaRepository _repository;

        public ConsultaRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new DataContext(options);
            _repository = new ConsultaRepository(_context);
        }

        private async Task SeedConsultas()
        {
            var consultas = new List<Consulta>
        {
            new Consulta { Id = 1, IdUsuarioPaciente = "paciente1", IdUsuarioMedico = "medico1", DataSolicitacao = DateTime.Now },
            new Consulta { Id = 2, IdUsuarioPaciente = "paciente1", IdUsuarioMedico = "medico2", DataSolicitacao = DateTime.Now },
            new Consulta { Id = 3, IdUsuarioPaciente = "paciente2", IdUsuarioMedico = "medico1", DataSolicitacao = DateTime.Now }
        };

            _context.Consultas.AddRange(consultas);
            await _context.SaveChangesAsync();
        }

        [Fact]
        [Trait("ConsultaRepository", "GetConsultaPorPaciente")]
        public async Task GetConsultaPorPaciente_DeveRetornarConsultasDoPaciente()
        {
            // Arrange
            await SeedConsultas();

            // Act
            var result = await _repository.GetConsultaPorPaciente("paciente1");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, c => Assert.Equal("paciente1", c.IdUsuarioPaciente));
        }

        [Fact]
        [Trait("ConsultaRepository", "Create")]
        public async Task Create_DeveAdicionarConsulta()
        {
            // Arrange
            var consulta = new Consulta
            {
                Id = 10,
                IdUsuarioPaciente = "pacienteX",
                IdUsuarioMedico = "medicoX",
                DataSolicitacao = DateTime.Today
            };

            // Act
            var result = await _repository.Create(consulta);

            // Assert
            Assert.Equal(consulta.Id, result.Id);
            Assert.Equal(1, _context.Consultas.Count());
        }

        [Fact]
        [Trait("ConsultaRepository", "Get (all)")]
        public async Task Get_DeveRetornarTodasConsultas()
        {
            // Arrange
            await SeedConsultas();

            // Act
            var result = await _repository.Get();

            // Assert
            Assert.Equal(3, result.Count);
        }

    }
}
