using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class StatusRepository : BaseRepository<Status>, IStatusRepository
    {
        private readonly DataContext _context;

        public StatusRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Status> Get(string nome)
        {
            var statuses = _context.Status.AsNoTracking();
            var item = await statuses.FirstOrDefaultAsync(x => x.Nome == nome);

            return item;
        }

        public async Task<List<Status>> SearchByNome(string nome)
        {
            var query = from status in _context.Status.AsNoTracking()
                        where status.Nome.Contains(nome)
                        select status;

            return await query.ToListAsync();
        }
    }
}
