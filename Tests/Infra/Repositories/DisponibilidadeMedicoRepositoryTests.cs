using Domain.Entities;
using global::Infra.Data.Context;
using global::Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infra.Repositories
{
    public class DisponibilidadeMedicoRepositoryTests
    {
        private readonly DataContext _context;
        private readonly DisponibilidadeMedicoRepository _repository;

        public DisponibilidadeMedicoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _repository = new DisponibilidadeMedicoRepository(_context);
        }

        private async Task SeedDisponibilidades()
        {
            var disponibilidades = new List<DisponibilidadeMedico>
        {
            new DisponibilidadeMedico { Id = 1, MedicoId = "medico1", Data = DateTime.Today, Ativo = true },
            new DisponibilidadeMedico { Id = 2, MedicoId = "medico1", Data = DateTime.Today.AddDays(1), Ativo = false },
            new DisponibilidadeMedico { Id = 3, MedicoId = "medico2", Data = DateTime.Today, Ativo = true }
        };

            _context.DisponibilidadeMedicos.AddRange(disponibilidades);
            await _context.SaveChangesAsync();
        }

        [Fact]
        [Trait("DisponibilidadeMedicoRepository", "AdicionarDisponibilidade")]
        public async Task AdicionarDisponibilidade_DeveAdicionar()
        {
            var disponibilidade = new DisponibilidadeMedico
            {
                Id = 10,
                MedicoId = "medicoX",
                Data = DateTime.Today,
                Ativo = true
            };

            var result = await _repository.AdicionarDisponibilidade(disponibilidade);

            Assert.Equal("medicoX", result.MedicoId);
            Assert.Equal(1, _context.DisponibilidadeMedicos.Count());
        }

        [Fact]
        [Trait("DisponibilidadeMedicoRepository", "AtualizarDisponibilidade")]
        public async Task AtualizarDisponibilidade_DeveAtualizar()
        {
            var disponibilidade = new DisponibilidadeMedico
            {
                Id = 20,
                MedicoId = "medicoY",
                Data = DateTime.Today,
                Ativo = true
            };

            await _repository.AdicionarDisponibilidade(disponibilidade);

            disponibilidade.Ativo = false;
            await _repository.AtualizarDisponibilidade(disponibilidade);

            var updated = await _repository.ObterDisponibilidade(20);
            Assert.False(updated.Ativo);
        }

        [Fact]
        [Trait("DisponibilidadeMedicoRepository", "ObterDisponibilidade(string)")]
        public async Task ObterDisponibilidade_AtivasPorMedico_DeveRetornarApenasAtivas()
        {
            await SeedDisponibilidades();

            var result = await _repository.ObterDisponibilidade("medico1");

            Assert.Single(result);
            Assert.True(result.All(d => d.Ativo));
        }

        [Fact]
        [Trait("DisponibilidadeMedicoRepository", "ObterAllDisponibilidade")]
        public async Task ObterAllDisponibilidade_DeveRetornarTodas()
        {
            await SeedDisponibilidades();

            var result = await _repository.ObterAllDisponibilidade("medico1");

            Assert.Equal(2, result.Count);
        }

        [Fact]
        [Trait("DisponibilidadeMedicoRepository", "ObterDisponibilidade(long)")]
        public async Task ObterDisponibilidadePorId_DeveRetornarCorreto()
        {
            await SeedDisponibilidades();

            var result = await _repository.ObterDisponibilidade(3);

            Assert.NotNull(result);
            Assert.Equal("medico2", result.MedicoId);
        }

        [Fact]
        [Trait("DisponibilidadeMedicoRepository", "ObterPorMedicoEData")]
        public async Task ObterPorMedicoEData_DeveFiltrarCorretamente()
        {
            await SeedDisponibilidades();

            var result = await _repository.ObterPorMedicoEData("medico1", DateTime.Today);

            Assert.Single(result);
            Assert.Equal("medico1", result[0].MedicoId);
            Assert.Equal(DateTime.Today, result[0].Data.Date);
        }
    }

}
