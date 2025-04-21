using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.Application.Services
{

    public class MedicoServiceTests
    {
        private readonly Mock<IEspecialidadeRepository> _especialidadeRepoMock = new();
        private readonly Mock<IDisponibilidadeMedicoRepository> _disponibilidadeRepoMock = new();
        private readonly Mock<UserManager<UserIdentity>> _userManagerMock;

        public MedicoServiceTests()
        {
            var store = new Mock<IUserStore<UserIdentity>>();
            _userManagerMock = new Mock<UserManager<UserIdentity>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private MedicoService CriarService() =>
            new(_especialidadeRepoMock.Object, _disponibilidadeRepoMock.Object, _userManagerMock.Object);

        [Fact]
        [Trait("MedicoService", "AdicionarDisponibilidade - Sucesso")]
        public async Task AdicionarDisponibilidade_DeveRetornarSucesso_QuandoNaoHouverConflito()
        {
            // Arrange
            var service = CriarService();
            var email = "medico@email.com";
            var user = new UserIdentity { Id = "medicoId" };
            var novaDisponibilidade = new DisponibilidadeMedico
            {
                Data = DateTime.Today,
                HoraInicio = new TimeSpan(10, 0, 0),
                HoraFim = new TimeSpan(11, 0, 0)
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _disponibilidadeRepoMock.Setup(x => x.ObterPorMedicoEData(user.Id, novaDisponibilidade.Data))
                .ReturnsAsync(new List<DisponibilidadeMedico>());
            _disponibilidadeRepoMock.Setup(x => x.AdicionarDisponibilidade(It.IsAny<DisponibilidadeMedico>()))
                .ReturnsAsync(novaDisponibilidade);

            // Act
            var result = await service.AdicionarDisponibilidade(novaDisponibilidade, email);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        [Trait("MedicoService", "AdicionarDisponibilidade - Conflito")]
        public async Task AdicionarDisponibilidade_DeveRetornarErro_QuandoHouverConflito()
        {
            // Arrange
            var service = CriarService();
            var email = "medico@email.com";
            var user = new UserIdentity { Id = "medicoId" };
            var novaDisponibilidade = new DisponibilidadeMedico
            {
                Data = DateTime.Today,
                HoraInicio = new TimeSpan(10, 0, 0),
                HoraFim = new TimeSpan(11, 0, 0)
            };

            var conflito = new DisponibilidadeMedico
            {
                HoraInicio = new TimeSpan(10, 30, 0),
                HoraFim = new TimeSpan(11, 30, 0)
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _disponibilidadeRepoMock.Setup(x => x.ObterPorMedicoEData(user.Id, novaDisponibilidade.Data))
                .ReturnsAsync(new List<DisponibilidadeMedico> { conflito });

            // Act
            var result = await service.AdicionarDisponibilidade(novaDisponibilidade, email);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Já existe disponibilidade registrada", result.Errors.First());
        }

        [Fact]
        [Trait("MedicoService", "ObterDisponibilidade")]
        public async Task ObterDisponibilidade_DeveRetornarLista_QuandoExistir()
        {
            // Arrange
            var service = CriarService();
            var email = "medico@email.com";
            var user = new UserIdentity { Id = "medicoId" };
            var lista = new List<DisponibilidadeMedico> { new() { Id = 1 } };

            _userManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _disponibilidadeRepoMock.Setup(x => x.ObterDisponibilidade(user.Id)).ReturnsAsync(lista);

            // Act
            var result = await service.ObterDisponibilidade(email);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(lista, result.Data);
        }

        [Fact]
        [Trait("MedicoService", "ObterDisponibilidadeid")]
        public async Task ObterDisponibilidadeid_DeveRetornarItem_QuandoExistir()
        {
            // Arrange
            var service = CriarService();
            var id = 1L;
            var disponibilidade = new DisponibilidadeMedico { Id = id };

            _disponibilidadeRepoMock.Setup(x => x.ObterDisponibilidade(id)).ReturnsAsync(disponibilidade);

            // Act
            var result = await service.ObterDisponibilidadeid(id);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(disponibilidade, result.Data);
        }

        [Fact]
        [Trait("MedicoService", "ObterTodasEspecialidades")]
        public async Task ObterTodasEspecialidades_DeveRetornarLista()
        {
            // Arrange
            var service = CriarService();
            var especialidades = new List<Especialidade> { new() { Id = 1, DsEspecialidade = "Cardiologia" } };

            _especialidadeRepoMock.Setup(x => x.ObterTodasEspecialidades()).ReturnsAsync(especialidades);

            // Act
            var result = await service.ObterTodasEspecialidades();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(especialidades, result.Data);
        }

        [Fact]
        [Trait("MedicoService", "RemoverDisponibilidade")]
        public async Task RemoverDisponibilidade_DeveRetornarSucesso()
        {
            // Arrange
            var service = CriarService();
            var id = 1L;

            _disponibilidadeRepoMock.Setup(x => x.RemoverDisponibilidade(id)).Returns(Task.CompletedTask);

            // Act
            var result = await service.RemoverDisponibilidade(id);

            // Assert
            Assert.True(result.Success);
        }
    }


}
