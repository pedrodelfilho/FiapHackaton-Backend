using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task<Status> Get(string nome);
        Task<List<Status>> SearchByNome(string nome);
    }
}
