using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infra.Data.Context;

namespace Infra.Data.Repositories
{
    public class SolicitacaoExameRepository : BaseRepository<SolicitacaoExame>, ISolicitacaoExameRepository
    {
        private readonly DataContext _context;

        public SolicitacaoExameRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
