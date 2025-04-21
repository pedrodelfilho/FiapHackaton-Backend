using Domain.Entities;
using Domain.Entities.Request;
using Domain.Entities.Response;

namespace Domain.Interfaces.Services
{
    public interface IConsultaService
    {
        Task<Consulta> RegistrarNovaConsulta(SolicitacaoConsultaRequest consulta);
        Task<List<Consulta>> ObterConsultaPorPaciente(string emailPaciente);
        Task<List<AgendamentoAprovacaoResponse>> ObterConsultaPendenteAprovacao(string email);
        Task<Consulta> ObterConsultaPorId(long idConsulta);
        Task EnviarEmail(EmailRequest emailRequest);
        Task<Consulta> AtualizarStatusConsulta(AtualizarStatusConsultaRequest atualizarStatusConsulta);
    }
}
