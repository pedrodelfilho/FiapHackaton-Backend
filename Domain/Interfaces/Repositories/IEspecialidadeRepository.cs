using Domain.Entities;
using Domain.Entities.Response;

namespace Domain.Interfaces.Repositories
{
    public interface IEspecialidadeRepository
    {
        Task<List<Especialidade>> ObterTodasEspecialidades();
    }
}
