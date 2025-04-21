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
    public class ConsultaController : ApiControllerBase
    {
        private readonly IConsultaService _consultaService;

        /// <summary>
        /// Controller responsável pela solicitação do agendamento
        /// </summary>
        /// <param name="consultaService"></param>
        /// <param name="identityService"></param>
        /// <param name="mapper"></param>
        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
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
        public async Task<ActionResult> CadastrarNovaConsulta(SolicitacaoConsultaRequest solicitacaoConsultaRequest)
        {
            try
            {
                var solicitacaoAgendamentoCreated = await _consultaService.RegistrarNovaConsulta(solicitacaoConsultaRequest);

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
        /// Comando responsável por cadastrar nova solicitação de agendamento
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("obter")]
        public async Task<ActionResult> ObterConsulta(string email)
        {
            try
            {
                var solicitacaoAgendamentoCreated = await _consultaService.ObterConsultaPorPaciente(email);

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
        /// Comando responsável por cadastrar nova solicitação de agendamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("obterid")]
        public async Task<ActionResult> ObterConsulta(long id)
        {
            try
            {
                var solicitacaoAgendamentoCreated = await _consultaService.ObterConsultaPorId(id);

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
        /// Comando responsável por cadastrar nova solicitação de agendamento
        /// </summary>
        /// <param name="emailRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("email")]
        public async Task<ActionResult> EnviarEmail(EmailRequest emailRequest)
        {
            try
            {
                await _consultaService.EnviarEmail(emailRequest);

                return Ok(new BaseResponse
                {
                    Message = "E-mail enviado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = null
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
        /// Comando responsável por cadastrar nova solicitação de agendamento
        /// </summary>
        /// <param name="atualizarStatusConsulta"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPut("atualizarstatus")]
        public async Task<ActionResult> AtualizarStatusConsulta(AtualizarStatusConsultaRequest atualizarStatusConsulta)
        {
            try
            {
                var consulta = await _consultaService.AtualizarStatusConsulta(atualizarStatusConsulta);

                return Ok(new BaseResponse
                {
                    Message = "Consulta atualizada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = consulta
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
        /// Comando responsável por cadastrar nova solicitação de agendamento
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Constants.Medico))]
        [HttpGet("obterconsultapendente")]
        public async Task<ActionResult> ObterConsultaPendenteAprovacao(string email)
        {
            try
            {
                var solicitacaoAgendamentoCreated = await _consultaService.ObterConsultaPendenteAprovacao(email);

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
    }
}
