using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infra.Data.Context;

namespace Infra.Data.Repositories
{
    public class SolicitacaoAgendamentoRepository : BaseRepository<SolicitacaoAgendamento>, ISolicitacaoAgendamentoRepository
    {
        private readonly DataContext _context;

        public SolicitacaoAgendamentoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
