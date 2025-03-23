using Domain.Interfaces.Repositories;
using Domain.Entities;

namespace Infra.Data.Repositories
{
    public class ResultadoRepository : IResultadoRepository
    {
        public Task<Resultado> Create(Resultado obj)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> Get(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Resultado>> Get()
        {
            throw new NotImplementedException();
        }

        public Task Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Resultado> Update(Resultado obj)
        {
            throw new NotImplementedException();
        }
    }
}
