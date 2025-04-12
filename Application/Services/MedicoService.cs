using Domain.Entities;
using Domain.Entities.Response;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IEspecialidadeRepository _especialidadeRepository;
        private readonly IDisponibilidadeMedicoRepository _disponibilidadeMedicoRepository;
        private readonly UserManager<UserIdentity> _userManager;

        public MedicoService(IEspecialidadeRepository medicoRepository, IDisponibilidadeMedicoRepository disponibilidadeMedicoRepository, UserManager<UserIdentity> userManager)
        {
            _especialidadeRepository = medicoRepository;
            _disponibilidadeMedicoRepository = disponibilidadeMedicoRepository;
            _userManager = userManager;
        }

        public async Task<BaseResponse> AdicionarDisponibilidade(DisponibilidadeMedico disponibilidade, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            disponibilidade.MedicoId = user.Id;

            var disponibilidadesExistentes = await _disponibilidadeMedicoRepository
                .ObterPorMedicoEData(user.Id, disponibilidade.Data);

            bool haConflito = disponibilidadesExistentes.Any(d =>
                (disponibilidade.HoraInicio <= d.HoraFim && disponibilidade.HoraFim >= d.HoraInicio)
            );

            if (haConflito)
            {
                var response = new BaseResponse(false);
                response.AdicionarErros(["Já existe disponibilidade registrada para essa data e hora!"]);
                return response;
            }
            else
            {
                var result = await _disponibilidadeMedicoRepository.AdicionarDisponibilidade(disponibilidade);

                if (result == null)
                    return new BaseResponse(false);

                var response = new DisponibilidadeMedicoResponse
                {
                    Id = result.MedicoId,
                    Data = result.Data,
                    HoraInicio = result.HoraInicio,
                    HoraFim = result.HoraFim
                };

                return new BaseResponse(true)
                {
                    Data = response
                };
            }           
        }

        public async Task<BaseResponse> ObterDisponibilidade(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var disponibilidades = await _disponibilidadeMedicoRepository.ObterDisponibilidade(user.Id);

            return new BaseResponse(disponibilidades != null)
            {
                Data = disponibilidades
            };
        }

        public async Task<BaseResponse> ObterTodasEspecialidades()
        {
            var especialidades = await _especialidadeRepository.ObterTodasEspecialidades();

            return new BaseResponse(especialidades != null)
            {
                Data = especialidades
            };
        }

        public async Task<BaseResponse> RemoverDisponibilidade(long idDisponibilidade)
        {
            await _disponibilidadeMedicoRepository.RemoverDisponibilidade(idDisponibilidade);

            return new BaseResponse(true);
        }
    }
}
