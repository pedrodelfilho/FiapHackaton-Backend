using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class TiposExameService : ITiposExameService
    {
        private readonly ITiposExameRepository _tiposExameRepository;

        public TiposExameService(ITiposExameRepository tiposExameRepository)
        {
            _tiposExameRepository = tiposExameRepository;
        }
        public async Task<TiposExame> Create(TiposExame tiposExame)
        {
            return await _tiposExameRepository.Create(tiposExame);
        }

        public async Task<TiposExame> Get(long id)
        {
            return await _tiposExameRepository.Get(id);
        }

        public async Task<List<TiposExame>> Get()
        {
            return await _tiposExameRepository.Get();
        }

        public async Task Remove(long id)
        {
            await _tiposExameRepository.Remove(id);
        }

        public async Task<List<TiposExame>> SearchByNome(string nome)
        {
            return await _tiposExameRepository.SearchByNome(nome);
        }

        public async Task<TiposExame> Update(TiposExame tiposExame)
        {
            return await _tiposExameRepository.Update(tiposExame);
        }
    }
}
