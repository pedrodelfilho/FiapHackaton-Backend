using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface ITiposExameRepository : IBaseRepository<TiposExame>
    {
        Task<List<TiposExame>> SearchByNome(string nome);
    }
}
