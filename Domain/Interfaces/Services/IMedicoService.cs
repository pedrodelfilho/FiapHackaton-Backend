using Domain.Entities;
using Domain.Entities.Response;

namespace Domain.Interfaces.Services
{
    public interface IMedicoService
    {
        Task<BaseResponse> ObterTodasEspecialidades();

        Task<BaseResponse> ObterDisponibilidade(string idMedico);

        Task<BaseResponse> AdicionarDisponibilidade(DisponibilidadeMedico disponibilidade, string email);
        Task<BaseResponse>RemoverDisponibilidade(long idDisponibilidade);
    }
}
