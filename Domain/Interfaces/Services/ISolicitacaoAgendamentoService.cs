using Domain.Entities;
using Domain.Entities.Request;

namespace Domain.Interfaces.Services
{
    public interface ISolicitacaoAgendamentoService
    {
        Task<SolicitacaoAgendamento> Create(SolicitacaoAgendamento solicitacao);
        Task<SolicitacaoAgendamento> Update(SolicitacaoAgendamento solicitacao, SolicitacaoAgendamentoUpdateStatusRequest updateStatusRequest);
        Task Remove(long id);
        Task<SolicitacaoAgendamento> Get(long id);
        Task<List<SolicitacaoAgendamento>> Get();
    }
}
