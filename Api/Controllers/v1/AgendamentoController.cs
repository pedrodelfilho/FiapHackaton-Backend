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

namespace Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    public class AgendamentoController : ApiControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controle responsável pelo agendamento
        /// </summary>
        /// <param name="agendamentoService"></param>
        /// <param name="mapper"></param>
        public AgendamentoController(IAgendamentoService agendamentoService, IMapper mapper)
        {
            _agendamentoService = agendamentoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Controle responsável por cadastrar novo agendamento
        /// </summary>
        /// <param name="agendamentoRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("cadastrar")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> CadastrarAgedamento([FromForm] AgendamentoRequest agendamentoRequest)
        {
            try
            {
                using var scope = new TransactionScope();

                var agendamento = _mapper.Map<Agendamento>(agendamentoRequest);
                var agendamentoCreated = await _agendamentoService.Create(agendamento);

                scope.Complete();
                return Ok(new BaseResponse
                {
                    Message = "Solicitação cadastrada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = agendamentoCreated
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
        /// Comando responsável por remover agendamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Constants.Administrador))]
        [HttpDelete("remover")]
        public async Task<ActionResult> RemoverAgendamento(long id)
        {
            try
            {
                await _agendamentoService.Remove(id);

                return Ok(new BaseResponse
                {
                    Message = "Agendamento removido com sucesso!",
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
        /// Comando responsável em atualizar agendamento
        /// </summary>
        /// <param name="agendamentoRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpPut("atualizarstatus")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AtualizarAgendamento([FromForm] AgendamentoRequest agendamentoRequest)
        {
            try
            {
                using var scope = new TransactionScope();

                var agendamento = _mapper.Map<Agendamento>(agendamentoRequest);
                var agendamentoUpdate = await _agendamentoService.Update(agendamento);

                scope.Complete();
                return Ok(new BaseResponse
                {
                    Message = "Agendamento atualizado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = agendamentoUpdate
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
        [Authorize]
        [HttpGet("obtertodos")]
        public async Task<ActionResult> ObterTodasSolicitacaoAgendamento()
        {
            try
            {
                var allAgendamentos = await _agendamentoService.Get();
                
                return Ok(new BaseResponse
                {
                    Message = "Busca por agendamentos realizado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = allAgendamentos
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
        [Authorize]
        [HttpGet("obterporid")]
        public async Task<ActionResult> ObterAgendamentoPorId(long id)
        {
            try
            {
                var allSolicitacoes = await _agendamentoService.Get(id);

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
