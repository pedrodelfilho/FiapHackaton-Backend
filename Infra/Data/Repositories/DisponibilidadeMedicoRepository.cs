using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infra.Data.Context;

namespace Infra.Data.Repositories
{
    public class DisponibilidadeMedicoRepository(DataContext dataContext) : BaseRepository<DisponibilidadeMedico>(dataContext), IDisponibilidadeMedicoRepository
    {
        public async Task<DisponibilidadeMedico> AdicionarDisponibilidade(DisponibilidadeMedico disponibilidade)
        {
            return await base.Create(disponibilidade);
        }

        public async Task AtualizarDisponibilidade(DisponibilidadeMedico disponibilidadeMedico)
        {
            await base.Update(disponibilidadeMedico);
        }

        public async Task<List<DisponibilidadeMedico>> ObterDisponibilidade(string idMedico)
        {
            var result = await base.Get();
            return result.Where(x => x.MedicoId == idMedico && x.Ativo).ToList();
        }

        public async Task<List<DisponibilidadeMedico>> ObterAllDisponibilidade(string idMedico)
        {
            var result = await base.Get();
            return result.Where(x => x.MedicoId == idMedico).ToList();
        }

        public async Task<DisponibilidadeMedico> ObterDisponibilidade(long idDisponibilidade)
        {
            return await base.Get(idDisponibilidade);
        }

        public async Task<List<DisponibilidadeMedico>> ObterPorMedicoEData(string medicoId, DateTime data)
        {
            var result = await base.Get();
            return result.Where(d => d.MedicoId == medicoId && d.Data.Date == data.Date).ToList();
        }

        public async Task RemoverDisponibilidade(long idDisponibilidade)
        {
            await base.Remove(idDisponibilidade);
        }
    }
}
