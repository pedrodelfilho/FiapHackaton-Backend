using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.Context;

namespace Infra.Data.Repositories
{
    public class EspecialidadeRespository(DataContext dataContext) : BaseRepository<Especialidade>(dataContext), IEspecialidadeRepository
    {
        public async Task<List<Especialidade>> ObterTodasEspecialidades()
        {
            var result = await base.Get(); 
            return [.. result];
        }
    }
}
