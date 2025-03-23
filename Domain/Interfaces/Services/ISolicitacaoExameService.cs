using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ISolicitacaoExameService
    {
        Task<SolicitacaoExame> Create(SolicitacaoExame solicitacaoExame);
        Task<SolicitacaoExame> Update(SolicitacaoExame solicitacaoExame);
        Task Remove(long id);
        Task<SolicitacaoExame> Get(long id);
        Task<List<SolicitacaoExame>> Get();
    }
}
