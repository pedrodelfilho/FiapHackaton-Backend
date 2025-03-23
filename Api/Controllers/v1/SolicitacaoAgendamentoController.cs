using Api.Controllers.Shared;
using Domain.Interfaces.Services;
using Application.Resource;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace TechChallenge.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    public class SolicitacaoAgendamentoController : ApiControllerBase
    {
        private readonly ISolicitacaoAgendamentoService _solicitacaoAgendamentoService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller responsável pela solicitação do agendamento
        /// </summary>
        /// <param name="solicitacaoAgendamentoService"></param>
        /// <param name="identityService"></param>
        /// <param name="mapper"></param>
        public SolicitacaoAgendamentoController(ISolicitacaoAgendamentoService solicitacaoAgendamentoService, IIdentityService identityService, IMapper mapper)
        {
            _solicitacaoAgendamentoService = solicitacaoAgendamentoService;
            _identityService = identityService;
            _mapper = mapper;
        }

        /// <summary>
        /// Comando responsável por cadastrar nova solicitação de agendamento
        /// </summary>
        /// <param name="solicitacaoAgendamentoRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("cadastrar")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> CadastrarSolicitacaoAgedamento([FromForm] SolicitacaoAgendamentoRequest solicitacaoAgendamentoRequest)
        {
            try
            {
                using var scope = new TransactionScope();
                var resultado = await _identityService.AlterarPerfilUsuario(solicitacaoAgendamentoRequest.Paciente, Constants.Paciente);

                var solicitacaoAgendamento = _mapper.Map<SolicitacaoAgendamento>(solicitacaoAgendamentoRequest);
                var solicitacaoAgendamentoCreated = await _solicitacaoAgendamentoService.Create(solicitacaoAgendamento);

                scope.Complete();
                return Ok(new BaseResponse
                {
                    Message = "Solicitação cadastrada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = solicitacaoAgendamentoCreated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Comando responsável em remover solicitação de agendamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Constants.Admin))]
        [HttpDelete("remover")]
        public async Task<ActionResult> RemoverSolicitacaoAgendamento(long id)
        {
            try
            {
                await _solicitacaoAgendamentoService.Remove(id);

                return Ok(new BaseResponse
                {
                    Message = "Solicitação removida com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = id
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Comando responsável por atualizar a situação do agendamento
        /// </summary>
        /// <param name="solicitacaoAgendamentoRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpPut("atualizarstatus")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AtualizarStatusSolicitacaoAgedamento([FromForm] SolicitacaoAgendamentoUpdateStatusRequest solicitacaoAgendamentoRequest)
        {
            try
            {
                using var scope = new TransactionScope();
                if (solicitacaoAgendamentoRequest.IdStatus == Constants.SOLICITACAO_AUTORIZADO && solicitacaoAgendamentoRequest.Exames.Count < 1)
                    return BadRequest(ResponsesExceptions.DomainErrorMessage("Nenhum exame selecionado!"));

                var solicitacaoAgendamento = await _solicitacaoAgendamentoService.Get(solicitacaoAgendamentoRequest.Id);

                var solicitacaoAgendamentoUpdate = await _solicitacaoAgendamentoService.Update(solicitacaoAgendamento, solicitacaoAgendamentoRequest);

                scope.Complete();
                return Ok(new BaseResponse
                {
                    Message = "Solicitação atualizada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = solicitacaoAgendamentoUpdate
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Comando responsável em obter todas as solicitações de agendamento registradas
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpGet("obtertodos")]
        public async Task<ActionResult> ObterTodasSolicitacaoAgendamento()
        {
            try
            {
                var tiposExamesLocalizados = await _solicitacaoAgendamentoService.Get();

                return Ok(new BaseResponse
                {
                    Message = "Busca por solicitações de agendamento realizada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = tiposExamesLocalizados
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Comando responsável por obter solicitação de agendamento pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpGet("obterporid")]
        public async Task<ActionResult> ObterSolicitacaoAgendamentoPorId(long id)
        {
            try
            {
                var allSolicitacoes = await _solicitacaoAgendamentoService.Get(id);

                return Ok(new BaseResponse
                {
                    Message = $"Pesquisa realizada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = allSolicitacoes
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }
    }
}
