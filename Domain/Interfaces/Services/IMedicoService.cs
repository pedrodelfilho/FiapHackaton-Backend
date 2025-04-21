using Domain.Entities;
using Domain.Entities.Response;

namespace Domain.Interfaces.Services
{
    public interface IMedicoService
    {
        Task<BaseResponse> ObterTodasEspecialidades();
        Task<BaseResponse> ObterDisponibilidade(string idMedico);
        Task<BaseResponse> ObterDisponibilidadeid(long idDisponibilidade);
        Task<BaseResponse> AdicionarDisponibilidade(DisponibilidadeMedico disponibilidade, string email);
        Task<BaseResponse>RemoverDisponibilidade(long idDisponibilidade);
        Task<BaseResponse> ObterMedicosPorEspecialidade(long idEspecialidade);
    }
}
