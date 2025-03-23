using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<Status> Create(Status status)
        {
            return await _statusRepository.Create(status);
        }

        public async Task<Status> Get(long id)
        {
            return await _statusRepository.Get(id);
        }

        public async Task<List<Status>> Get()
        {
            return await _statusRepository.Get();
        }

        public async Task Remove(long id)
        {
            await _statusRepository.Remove(id);
        }

        public async Task<List<Status>> SearchByNome(string nome)
        {
            return await _statusRepository.SearchByNome(nome);
        }

        public async Task<Status> Update(Status status)
        {
            return await _statusRepository.Update(status);
        }
    }
}
