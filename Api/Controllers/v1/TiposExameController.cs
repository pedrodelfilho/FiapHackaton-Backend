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
    public class TiposExameController : ApiControllerBase
    {
        private readonly ITiposExameService _tiposExameService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tiposExameService"></param>
        /// <param name="mapper"></param>
        public TiposExameController(ITiposExameService tiposExameService, IMapper mapper)
        {
            _tiposExameService = tiposExameService;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tiposExameRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarTiposExame([FromBody] TiposExameRequest tiposExameRequest)
        {
            try
            {
                var tiposExameExist = await _tiposExameService.SearchByNome(tiposExameRequest.Nome);
                if (tiposExameExist.Count > 0) { return BadRequest($"Já existe tipos de exame criado com o nome {tiposExameRequest.Nome}!"); }

                var tiposExame = _mapper.Map<TiposExame>(tiposExameRequest);
                var tiposExameCreated = await _tiposExameService.Create(tiposExame);

                return Ok(new BaseResponse
                {
                    Message = "Exame cadastrado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = tiposExameCreated
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
        public async Task<ActionResult> RemoverTiposExame(long id)
        {
            try
            {
                await _tiposExameService.Remove(id);

                return Ok(new BaseResponse
                {
                    Message = "Exame removido com sucesso!",
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
        /// <param name="tiposExameRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpPut("atualizar")]
        public async Task<ActionResult> AtualizarTiposExame([FromBody] TiposExameUpdateRequest tiposExameRequest)
        {
            try
            {
                var tiposExameExist = await _tiposExameService.SearchByNome(tiposExameRequest.Nome);
                if (tiposExameExist.Count < 0) { return BadRequest($"Convênio {tiposExameRequest.Nome}, não localizado!"); }

                var exame = new TiposExame(tiposExameExist[0].Id, tiposExameRequest.NovoNome);

                var tiposExameAtualizado = await _tiposExameService.Update(exame);

                return Ok(new BaseResponse
                {
                    Message = "Exame atualizado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = tiposExameAtualizado
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
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpGet("obtertodos")]
        public async Task<ActionResult> ObterTodosTiposExames()
        {
            try
            {
                var tiposExamesLocalizados = await _tiposExameService.Get();

                return Ok(new BaseResponse
                {
                    Message = "Busca por tipos de exames realizada com sucesso!",
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
        /// 
        /// </summary>
        /// <param name="tiposExame"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpGet("obterpornome")]
        public async Task<ActionResult> ObterTiposExamesPorNome([FromBody] TiposExameRequest tiposExame)
        {
            try
            {
                var tiposExameLocalizado = await _tiposExameService.SearchByNome(tiposExame.Nome);

                return Ok(new BaseResponse
                {
                    Message = $"Pesquisa pelo tipos de exame {tiposExame.Nome}, realizada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = tiposExameLocalizado
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
