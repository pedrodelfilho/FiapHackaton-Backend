using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IResultadoService
    {
        Task<Resultado> Create(Resultado resultadoDto);
        Task<Resultado> Update(Resultado resultadoDto);
        Task Remove(long id);
        Task<Resultado> Get(long id);
        Task<List<Resultado>> Get();
    }
}
