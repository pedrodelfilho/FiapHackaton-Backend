using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IAgendamentoService
    {
        Task<Agendamento> Create(Agendamento agendamento);
        Task<Agendamento> Update(Agendamento agendamento);
        Task Remove(long id);
        Task<Agendamento> Get(long id);
        Task<List<Agendamento>> Get();
    }
}
