using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class SolicitacaoExameService : ISolicitacaoExameService
    {
        private readonly ISolicitacaoExameRepository _SolicitacaoExameRepository;

        public SolicitacaoExameService(ISolicitacaoExameRepository solicitacaoExameRepository)
        {
            _SolicitacaoExameRepository = solicitacaoExameRepository;
        }

        public async Task<SolicitacaoExame> Create(SolicitacaoExame solicitacaoExame)
        {
            return await _SolicitacaoExameRepository.Create(solicitacaoExame);
        }

        public Task<SolicitacaoExame> Get(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SolicitacaoExame>> Get()
        {
            throw new NotImplementedException();
        }

        public Task Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Task<SolicitacaoExame> Update(SolicitacaoExame solicitacaoExame)
        {
            throw new NotImplementedException();
        }
    }
}
