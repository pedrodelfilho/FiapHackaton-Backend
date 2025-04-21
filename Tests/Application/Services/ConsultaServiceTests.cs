using Application.Services;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Entities.Request;
using Domain.Interfaces.Repositories;
using Infra.Mail;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Tests.Application.Services
{

    public class ConsultaServiceTests
    {
        private readonly Mock<IConsultaRepository> _consultaRepositoryMock;
        private readonly Mock<UserManager<UserIdentity>> _userManagerMock;
        private readonly Mock<IDisponibilidadeMedicoRepository> _disponibilidadeMedicoMock;
        private readonly Mock<IEnvioEmail> _envioEmailMock;
        private readonly Mock<IEspecialidadeRepository> _especialidadeRepositoryMock;
        private readonly ConsultaService _consultaService;

        public ConsultaServiceTests()
        {
            _consultaRepositoryMock = new Mock<IConsultaRepository>();
            _userManagerMock = MockUserManager();
            _disponibilidadeMedicoMock = new Mock<IDisponibilidadeMedicoRepository>();
            _envioEmailMock = new Mock<IEnvioEmail>();
            _especialidadeRepositoryMock = new Mock<IEspecialidadeRepository>();

            _consultaService = new ConsultaService(
                _consultaRepositoryMock.Object,
                _userManagerMock.Object,
                _disponibilidadeMedicoMock.Object,
                _envioEmailMock.Object,
                _especialidadeRepositoryMock.Object
            );
        }

        private static Mock<UserManager<UserIdentity>> MockUserManager()
        {
            var store = new Mock<IUserStore<UserIdentity>>();
            return new Mock<UserManager<UserIdentity>>(store.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        [Trait("ConsultaService", "Enviar email para um ou dois destinatários")]
        public async Task EnviarEmail_DeveEnviarParaUmOuDoisDestinatarios()
        {
            var request = new EmailRequest
            {
                EmailDestino1 = "destino1@teste.com",
                EmailDestino2 = "destino2@teste.com",
                Assunto = "Teste",
                Body = "Corpo"
            };

            await _consultaService.EnviarEmail(request);

            _envioEmailMock.Verify(e => e.EnviarEmailAviso("destino1@teste.com", "Teste", "Corpo"), Times.Once);
            _envioEmailMock.Verify(e => e.EnviarEmailAviso("destino2@teste.com", "Teste", "Corpo"), Times.Once);
        }

        [Fact]
        [Trait("ConsultaService", "Obter consultas por paciente")]
        public async Task ObterConsultaPorPaciente_DeveRetornarConsultas()
        {
            var user = new UserIdentity { Id = "123", Email = "teste@teste.com" };
            var consultas = new List<Consulta> { new Consulta() };

            _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _consultaRepositoryMock.Setup(x => x.GetConsultaPorPaciente("123")).ReturnsAsync(consultas);

            var result = await _consultaService.ObterConsultaPorPaciente(user.Email);

            Assert.Equal(consultas, result);
        }

        [Fact]
        [Trait("ConsultaService", "Obter consulta por ID")]
        public async Task ObterConsultaPorId_DeveRetornarConsulta()
        {
            var consulta = new Consulta { Id = 1 };
            _consultaRepositoryMock.Setup(x => x.Get(1)).ReturnsAsync(consulta);

            var result = await _consultaService.ObterConsultaPorId(1);

            Assert.Equal(consulta, result);
        }

        [Fact]
        [Trait("ConsultaService", "Registrar nova consulta")]
        public async Task RegistrarNovaConsulta_DeveCriarNovaConsulta()
        {
            var paciente = new UserIdentity { Id = "1", Email = "paciente@teste.com" };
            var medico = new UserIdentity { Id = "2", Email = "medico@teste.com", IdEspecialidade = 1 };
            var disponibilidade = new DisponibilidadeMedico { Id = 1, Ativo = true };

            var request = new SolicitacaoConsultaRequest
            {
                EmailPaciente = paciente.Email,
                EmailMedico = medico.Email,
                IdDisponibilidade = 1
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(paciente.Email)).ReturnsAsync(paciente);
            _userManagerMock.Setup(x => x.FindByEmailAsync(medico.Email)).ReturnsAsync(medico);
            _disponibilidadeMedicoMock.Setup(x => x.ObterDisponibilidade(1)).ReturnsAsync(disponibilidade);
            _consultaRepositoryMock.Setup(x => x.Create(It.IsAny<Consulta>())).ReturnsAsync(new Consulta());

            var result = await _consultaService.RegistrarNovaConsulta(request);

            Assert.NotNull(result);
        }

        [Fact]
        [Trait("ConsultaService", "Atualizar status da consulta")]
        public async Task AtualizarStatusConsulta_DeveAtualizarEReornarConsulta()
        {
            var consulta = new Consulta { Id = 1 };
            var request = new AtualizarStatusConsultaRequest { IdConsulta = 1, StatusConsulta = StatusConsulta.Autorizado };

            _consultaRepositoryMock.Setup(x => x.Get(1)).ReturnsAsync(consulta);
            _consultaRepositoryMock.Setup(x => x.Update(consulta));

            var result = await _consultaService.AtualizarStatusConsulta(request);

            Assert.Equal(consulta, result);
            Assert.Equal((byte)StatusConsulta.Autorizado, result.IdStatus);
        }

        [Fact]
        [Trait("ConsultaService", "Obter consultas pendentes de aprovação")]
        public async Task ObterConsultaPendenteAprovacao_DeveRetornarAgendamentos()
        {
            var medico = new UserIdentity { Id = "2", Email = "medico@teste.com", NomeCompleto = "Dr. Teste", IdEspecialidade = 1 };
            var paciente = new UserIdentity { Id = "1", NomeCompleto = "Paciente Teste" };
            var especialidades = new List<Especialidade> { new Especialidade { Id = 1, DsEspecialidade = "Cardiologia" } };
            var disponibilidades = new List<DisponibilidadeMedico>
        {
            new DisponibilidadeMedico
            {
                Id = 1,
                Data = DateTime.Today,
                HoraInicio = new TimeSpan(10, 0, 0),
                HoraFim = new TimeSpan(11, 0, 0)
            }
        };
            var consultas = new List<Consulta>
        {
            new Consulta { Id = 1, IdUsuarioMedico = "2", IdUsuarioPaciente = "1", IdDisponibilidade = 1, IdStatus = 1 }
        };

            _userManagerMock.Setup(x => x.FindByEmailAsync(medico.Email)).ReturnsAsync(medico);
            _especialidadeRepositoryMock.Setup(x => x.ObterTodasEspecialidades()).ReturnsAsync(especialidades);
            _disponibilidadeMedicoMock.Setup(x => x.ObterAllDisponibilidade(medico.Id)).ReturnsAsync(disponibilidades);
            _consultaRepositoryMock.Setup(x => x.Get()).ReturnsAsync(consultas);
            _userManagerMock.Setup(x => x.FindByIdAsync("1")).ReturnsAsync(paciente);

            var result = await _consultaService.ObterConsultaPendenteAprovacao(medico.Email);

            Assert.Single(result);
            Assert.Equal("Paciente Teste", result[0].PacienteNome);
        }
    }

}

