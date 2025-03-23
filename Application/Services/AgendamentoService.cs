using Application.Resource;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IHistoricoAgendamentoService _historicoAgendamentoService;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository, IHistoricoAgendamentoService historicoAgendamentoService)
        {
            _agendamentoRepository = agendamentoRepository;
            _historicoAgendamentoService = historicoAgendamentoService;
        }

        public async Task<Agendamento> Create(Agendamento agendamento)
        {
            var historico = Handler.Handler.BuildHistoricoAgendamento(agendamento, Constants.AGENDAMENTO_ATIVO);
            await _historicoAgendamentoService.Create(historico);

            return await _agendamentoRepository.Create(agendamento);
        }

        public async Task<Agendamento> Get(long id)
        {
            return await _agendamentoRepository.Get(id);
        }

        public async Task<List<Agendamento>> Get()
        {
            return await _agendamentoRepository.Get();
        }

        public async Task Remove(long id)
        {
            await _agendamentoRepository.Remove(id);
        }

        public async Task<Agendamento> Update(Agendamento agendamento)
        {
            var historico = Handler.Handler.BuildHistoricoAgendamento(agendamento, agendamento.IdStatus);
            await _historicoAgendamentoService.Create(historico);
            await _agendamentoRepository.Update(agendamento);
            return agendamento;
        }
    }
}
