using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IStatusService
    {
        Task<Status> Create(Status equipmentDTO);
        Task<Status> Update(Status equipmentDTO);
        Task Remove(long id);
        Task<Status> Get(long id);
        Task<List<Status>> Get();
        Task<List<Status>> SearchByNome(string nome);
    }
}
