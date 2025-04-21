using Domain.Entities;
using global::Infra.Data.Context;
using Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infra.Repositories
{


    public class EspecialidadeRepositoryTests
    {
        private readonly DataContext _context;
        private readonly EspecialidadeRespository _repository;

        public EspecialidadeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);
            _repository = new EspecialidadeRespository(_context);
        }

        private async Task SeedEspecialidades()
        {
            var especialidades = new List<Especialidade>
        {
            new Especialidade { Id = 1, DsEspecialidade = "Cardiologia" },
            new Especialidade { Id = 2, DsEspecialidade = "Dermatologia" },
            new Especialidade { Id = 3, DsEspecialidade = "Pediatria" }
        };

            _context.Especialidades.AddRange(especialidades);
            await _context.SaveChangesAsync();
        }

        [Fact]
        [Trait("EspecialidadeRepository", "ObterTodasEspecialidades")]
        public async Task ObterTodasEspecialidades_DeveRetornarTodas()
        {
            await SeedEspecialidades();

            var result = await _repository.ObterTodasEspecialidades();

            Assert.Equal(3, result.Count);
        }

        [Fact]
        [Trait("EspecialidadeRepository", "AdicionarEspecialidade")]
        public async Task AdicionarEspecialidade_DeveAdicionar()
        {
            var especialidade = new Especialidade { DsEspecialidade = "Neurologia" };

            var result = await _repository.Create(especialidade);

            Assert.Equal("Neurologia", result.DsEspecialidade);
            Assert.Equal(1, _context.Especialidades.Count());
        }

        [Fact]
        [Trait("EspecialidadeRepository", "AtualizarEspecialidade")]
        public async Task AtualizarEspecialidade_DeveAtualizar()
        {
            var especialidade = new Especialidade { Id = 5, DsEspecialidade = "Ginecologia" };
            await _repository.Create(especialidade);

            especialidade.DsEspecialidade = "Obstetrícia";
            var updatedEspecialidade = await _repository.Update(especialidade);

            Assert.Equal("Obstetrícia", updatedEspecialidade.DsEspecialidade);
        }
    }

}
