using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infra.Data.Context;

namespace Infra.Data.Repositories
{
    public class AgendamentoRepository : BaseRepository<Agendamento>, IAgendamentoRepository
    {
        private readonly DataContext _context;

        public AgendamentoRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
