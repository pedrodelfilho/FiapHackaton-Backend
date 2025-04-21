using Domain.Entities;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infra.Mail;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IDisponibilidadeMedicoRepository _disponibiliadeMedico;
        private readonly IEnvioEmail _envioEmail;
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public ConsultaService(IConsultaRepository consultaRepository, 
            UserManager<UserIdentity> userManager, 
            IDisponibilidadeMedicoRepository disponibiliadeMedico, 
            IEnvioEmail envioEmail,
            IEspecialidadeRepository especialidadeRepository)
        {
            _consultaRepository = consultaRepository;
            _userManager = userManager;
            _disponibiliadeMedico = disponibiliadeMedico;
            _envioEmail = envioEmail;
            _especialidadeRepository = especialidadeRepository;
        }

        public Task EnviarEmail(EmailRequest emailRequest)
        {
            _envioEmail.EnviarEmailAviso(emailRequest.EmailDestino1, emailRequest.Assunto, emailRequest.Body);
            if(emailRequest.EmailDestino2 != null)
                _envioEmail.EnviarEmailAviso(emailRequest.EmailDestino2, emailRequest.Assunto, emailRequest.Body);
            return Task.CompletedTask;
        }

        public async Task<List<Consulta>> ObterConsultaPorPaciente(string emailPaciente)
        {
            var paciente = await _userManager.FindByEmailAsync(emailPaciente);
            return await _consultaRepository.GetConsultaPorPaciente(paciente.Id);
        }

        public async Task<Consulta> ObterConsultaPorId(long idConsulta)
        {
            return await _consultaRepository.Get(idConsulta);
        }

        public async Task<Consulta> RegistrarNovaConsulta(SolicitacaoConsultaRequest SolicitacaoConsultaRequest)
        {
            try
            {
                var paciente = await _userManager.FindByEmailAsync(SolicitacaoConsultaRequest.EmailPaciente);
                var medico = await _userManager.FindByEmailAsync(SolicitacaoConsultaRequest.EmailMedico);
                var disponibilidade = await _disponibiliadeMedico.ObterDisponibilidade(SolicitacaoConsultaRequest.IdDisponibilidade);
                disponibilidade.Ativo = false;
                await _disponibiliadeMedico.AtualizarDisponibilidade(disponibilidade);
                var consulta = Consulta.FactoryConsulta(paciente.Id, medico.Id, disponibilidade.Id, 1);

                return await _consultaRepository.Create(consulta);
            }
            catch (Exception ex)
            {
                var a = ex;
                throw;
            }
            
        }

        public async Task<Consulta> AtualizarStatusConsulta(AtualizarStatusConsultaRequest atualizarStatusConsulta)
        {
            var consulta = await _consultaRepository.Get(atualizarStatusConsulta.IdConsulta);
            consulta.IdStatus = (byte)atualizarStatusConsulta.StatusConsulta;

            await _consultaRepository.Update(consulta);
            return consulta;
        }

        public async Task<List<AgendamentoAprovacaoResponse>> ObterConsultaPendenteAprovacao(string email)
        {
            var medico = await _userManager.FindByEmailAsync(email);
            var especialidades = await _especialidadeRepository.ObterTodasEspecialidades();
            var disponibilidade = await _disponibiliadeMedico.ObterAllDisponibilidade(medico.Id);
            var consultas = await _consultaRepository.Get();
            consultas.RemoveAll(x => x.IdUsuarioMedico != medico.Id);

            var agendamentos = new List<AgendamentoAprovacaoResponse>();

            foreach(var consulta in consultas)
            {
                var paciente = await _userManager.FindByIdAsync(consulta.IdUsuarioPaciente);

                agendamentos.Add(new AgendamentoAprovacaoResponse { 
                    Id = consulta.Id,
                    PacienteNome = paciente.NomeCompleto,
                    MedicoNome = medico.NomeCompleto,
                    Especialidade = especialidades.Where(x => x.Id == medico.IdEspecialidade).FirstOrDefault().DsEspecialidade,
                    Data = disponibilidade.Where(x => x.Id == consulta.IdDisponibilidade).FirstOrDefault().Data.ToString("dd/MM/yyyy"),
                    HoraInicio = disponibilidade.Where(x => x.Id == consulta.IdDisponibilidade).FirstOrDefault().HoraInicio.ToString(@"hh\:mm"),
                    HoraFim = disponibilidade.Where(x => x.Id == consulta.IdDisponibilidade).FirstOrDefault().HoraFim.ToString(@"hh\:mm"),
                    StatusDescricao = consulta.IdStatus.ToString()
                });
            }

            return agendamentos;
        }
    }
}
