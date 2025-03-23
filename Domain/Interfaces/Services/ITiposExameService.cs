using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface ITiposExameService
    {
        Task<TiposExame> Create(TiposExame tiposExameDTO);
        Task<TiposExame> Update(TiposExame tiposExameDTO);
        Task Remove(long id);
        Task<TiposExame> Get(long id);
        Task<List<TiposExame>> Get();
        Task<List<TiposExame>> SearchByNome(string nome);
    }
}
