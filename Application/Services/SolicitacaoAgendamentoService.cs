using Application.Resource;
using Application.Services.Handler;
using Domain.Entities;
using Domain.Entities.Request;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class SolicitacaoAgendamentoService : ISolicitacaoAgendamentoService
    {
        private readonly ISolicitacaoAgendamentoRepository _solicitacaoAgendamentoRepository;
        private readonly IHistoricoAgendamentoService _historicoAgendamentoService;
        private readonly ISolicitacaoExameService _solicitacaoExameService;
        private readonly IBlobStorageService _blobStorageService;

        public SolicitacaoAgendamentoService(ISolicitacaoAgendamentoRepository solicitacaoAgendamentoRepository, 
            IHistoricoAgendamentoService historicoAgendamentoService, ISolicitacaoExameService solicitacaoExameService, IBlobStorageService blobStorageService)
        {
            _solicitacaoAgendamentoRepository = solicitacaoAgendamentoRepository;
            _historicoAgendamentoService = historicoAgendamentoService;
            _solicitacaoExameService = solicitacaoExameService;
            _blobStorageService = blobStorageService;
        }

        public async Task<SolicitacaoAgendamento> Create(SolicitacaoAgendamento solicitacaoAgendamento)
        {
            solicitacaoAgendamento.SetArquivo(String.Concat(Constants.CONTAINER_GUIA_SOLICITACAO, "/", solicitacaoAgendamento.Paciente, solicitacaoAgendamento.FormFile.FileName));
            solicitacaoAgendamento.SetData(DateTime.Now);
            solicitacaoAgendamento.SetStatus(Constants.SOLICITACAO_ATIVA);

            using (var stream = new MemoryStream())
            {
                await solicitacaoAgendamento.FormFile.CopyToAsync(stream);
                stream.Position = 0;

                await _blobStorageService.SaveFileToBlobStorageAsync(Constants.CONTAINER_GUIA_SOLICITACAO, solicitacaoAgendamento.Paciente+solicitacaoAgendamento.FormFile.FileName, stream);
            }

            await _solicitacaoAgendamentoRepository.Create(solicitacaoAgendamento);
            var historicoAgendamento = Handler.Handler.BuildHistoricoAgendamento(solicitacaoAgendamento);
            await _historicoAgendamentoService.Create(historicoAgendamento);

            return solicitacaoAgendamento;
        }

        public async Task<SolicitacaoAgendamento> Get(long id)
        {
            return await _solicitacaoAgendamentoRepository.Get(id);
        }

        public async Task<List<SolicitacaoAgendamento>> Get()
        {
            return await _solicitacaoAgendamentoRepository.Get();
        }

        public async Task Remove(long id)
        {
            await _solicitacaoAgendamentoRepository.Remove(id);
        }

        public async Task<SolicitacaoAgendamento> Update(SolicitacaoAgendamento solicitacao, SolicitacaoAgendamentoUpdateStatusRequest updateStatusRequest)
        {
            solicitacao.SetStatus(updateStatusRequest.IdStatus);
            await _solicitacaoAgendamentoRepository.Update(solicitacao);

            if (updateStatusRequest.IdStatus == Constants.SOLICITACAO_AUTORIZADO)
            {
                foreach(var exame in updateStatusRequest.Exames)
                    await _solicitacaoExameService.Create(new SolicitacaoExame(solicitacao.Id, exame));
            }

            var historicoAgendamento = Handler.Handler.BuildHistoricoAgendamento(solicitacao);
            await _historicoAgendamentoService.Create(historicoAgendamento);

            return solicitacao;
        }
    }
}
