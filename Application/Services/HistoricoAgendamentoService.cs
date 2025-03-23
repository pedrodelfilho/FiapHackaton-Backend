using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class HistoricoAgendamentoService : IHistoricoAgendamentoService
    {
        private readonly IHistoricoAgendamentoRepository _historicoAgendamentoRepository;

        public HistoricoAgendamentoService(IHistoricoAgendamentoRepository historicoAgendamentoRepository)
        {
            _historicoAgendamentoRepository = historicoAgendamentoRepository;
        }

        public async Task<HistoricoAgendamento> Create(HistoricoAgendamento historico)
        {
            return await _historicoAgendamentoRepository.Create(historico);
        }

        public Task<HistoricoAgendamento> Get(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<HistoricoAgendamento>> Get()
        {
            throw new NotImplementedException();
        }

        public Task Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Task<HistoricoAgendamento> Update(HistoricoAgendamento historico)
        {
            throw new NotImplementedException();
        }
        public async Task<HistoricoAgendamento> SearchByIdSolicitacao(long idSolicitacao)
        {
            return await _historicoAgendamentoRepository.Get(idSolicitacao);
        }
    }
}
