using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IConvenioRepository : IBaseRepository<Convenio>
    {
        Task<Convenio> Get(string nome);
        Task<List<Convenio>> SearchByNome(string nome);
    }
}
