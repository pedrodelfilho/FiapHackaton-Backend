using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class ConvenioRepository : BaseRepository<Convenio>, IConvenioRepository
    {
        private readonly DataContext _context;

        public ConvenioRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Convenio> Get(string nome)
        {
            var convenios = _context.Convenios.AsNoTracking();
            var item = await convenios.FirstOrDefaultAsync(x => x.Nome == nome);
            return item;
        }

        public async Task<List<Convenio>> SearchByNome(string nome)
        {
            var query = from status in _context.Convenios.AsNoTracking()
                        where status.Nome.Contains(nome)
                        select status;

            return await query.ToListAsync();
        }
    }
}
