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

namespace TechChallenge.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    public class StatusController : ApiControllerBase
    {
        private readonly IStatusService _statusService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusService"></param>
        /// <param name="mapper"></param>
        public StatusController(IStatusService statusService, IMapper mapper)
        {
            _statusService = statusService;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarStatus([FromBody] StatusRequest statusRequest)
        {
            try
            {
                var statusExist = await _statusService.SearchByNome(statusRequest.Nome);
                if(statusExist.Count > 0) { return BadRequest($"Já existe um status criado com o nome {statusRequest.Nome}!"); }

                var status = _mapper.Map<Status>(statusRequest);
                var statusCreated = await _statusService.Create(status);

                return Ok(new BaseResponse
                {
                    Message = "Status cadastrado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = statusCreated
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Constants.Admin))]
        [HttpDelete("remover")]
        public async Task<ActionResult> RemoverStatus(long id)
        {
            try
            {
                await _statusService.Remove(id);

                return Ok(new BaseResponse
                {
                    Message = "Status removido com sucesso!",
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
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpPut("atualizar")]
        public async Task<ActionResult> AtualizarStatus([FromBody] StatusUpdateRequest status)
        {
            try
            {
                var statusExist = await _statusService.SearchByNome(status.Nome);
                if (statusExist.Count < 0) { return BadRequest($"Status {status.Nome}, não localizado!"); }

                var statusDTO = new Status { Nome = status.NovoNome, Id = statusExist[0].Id };

                var statusAtualizado = await _statusService.Update(statusDTO);

                return Ok(new BaseResponse
                {
                    Message = "Status atualizado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = statusAtualizado
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
        /// 
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<ActionResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpGet("obtertodos")]
        public async Task<ActionResult> ObterTodosStatus()
        {
            try
            {
                var statusLocalizados = await _statusService.Get();

                return Ok(new BaseResponse
                {
                    Message = "Busca pelos status realizado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = statusLocalizados
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
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<ActionResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpGet("obterpornome")]
        public async Task<ActionResult> ObterStatusPorNome([FromBody] StatusRequest status)
        {
            try
            {
                var statusLocalizado = await _statusService.SearchByNome(status.Nome);

                return Ok(new BaseResponse
                {
                    Message = $"Pesquisa pelo status {status.Nome}, realizada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = statusLocalizado
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
