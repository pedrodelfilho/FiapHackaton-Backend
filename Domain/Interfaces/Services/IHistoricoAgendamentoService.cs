using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IHistoricoAgendamentoService
    {
        Task<HistoricoAgendamento> Create(HistoricoAgendamento historicoDto);
        Task<HistoricoAgendamento> Update(HistoricoAgendamento historicoDto);
        Task Remove(long id);
        Task<HistoricoAgendamento> Get(long id);
        Task<List<HistoricoAgendamento>> Get();
        Task<HistoricoAgendamento> SearchByIdSolicitacao(long idSolicitacao);
    }
}
