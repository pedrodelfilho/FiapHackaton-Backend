using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infra.Data.Context;

namespace Infra.Data.Repositories
{
    public class HistoricoAgendamentoRepository : BaseRepository<HistoricoAgendamento>, IHistoricoAgendamentoRepository
    {
        private readonly DataContext _context;

        public HistoricoAgendamentoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<HistoricoAgendamento> SearchByIdSolicitacao(long id)
        {
            var query = from historico in _context.HistoricoSolicitalcaos.AsNoTracking()
                        where historico.Id == id
                        select historico;

            return await query.FirstAsync();
        }
    }
}
