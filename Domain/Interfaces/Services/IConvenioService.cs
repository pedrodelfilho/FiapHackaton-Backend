using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IConvenioService
    {
        Task<Convenio> Create(Convenio convenioDTO);
        Task<Convenio> Update(Convenio convenioDTO);
        Task Remove(long id);
        Task<Convenio> Get(long id);
        Task<List<Convenio>> Get();
        Task<List<Convenio>> SearchByNome(string nome);
    }
}
