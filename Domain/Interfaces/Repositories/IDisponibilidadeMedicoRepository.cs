using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IDisponibilidadeMedicoRepository
    {
        Task<List<DisponibilidadeMedico>> ObterDisponibilidade(string idMedico);
        Task<DisponibilidadeMedico> AdicionarDisponibilidade(DisponibilidadeMedico disponibilidade);
        Task RemoverDisponibilidade(long idDisponibilidade);
        Task<List<DisponibilidadeMedico>> ObterPorMedicoEData(string medicoId, DateTime data);
    }
}
