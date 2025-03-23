using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infra.Data.Context;

namespace Infra.Data.Repositories
{
    public class TiposExameRepository : BaseRepository<TiposExame>, ITiposExameRepository
    {
        private readonly DataContext _context;

        public TiposExameRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TiposExame>> SearchByNome(string nome)
        {
            var query = from status in _context.TiposExames.AsNoTracking()
                        where status.Nome.Contains(nome)
                        select status;

            return await query.ToListAsync();
        }
    }
}
