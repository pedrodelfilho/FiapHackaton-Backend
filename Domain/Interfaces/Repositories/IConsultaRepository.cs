using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IConsultaRepository : IBaseRepository<Consulta> 
    {
        Task<List<Consulta>> GetConsultaPorPaciente(string idPaciente);
    }
}
