using Domain.Interfaces.Repositories;
using Domain.Entities;
using Infra.Data.Context;
using System.ComponentModel.DataAnnotations;

namespace Infra.Data.Repositories
{
    public class ConsultaRepository : BaseRepository<Consulta>, IConsultaRepository
    {
        private readonly DataContext _context;

        public ConsultaRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Consulta>> GetConsultaPorPaciente(string idPaciente)
        {
            var result = await base.Get();
            return result.Where(x => x.IdUsuarioPaciente == idPaciente).ToList();
        }
    }
}
