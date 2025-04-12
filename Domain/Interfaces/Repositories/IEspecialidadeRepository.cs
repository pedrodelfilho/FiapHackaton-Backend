using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IEspecialidadeRepository
    {
        Task<List<Especialidade>> ObterTodasEspecialidades();
    }
}
