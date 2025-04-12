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

namespace Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    public class ConvenioController : ApiControllerBase
    {
        private readonly IConvenioService _convenioService;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="convenioService"></param>
        /// <param name="mapper"></param>
        public ConvenioController(IConvenioService convenioService, IMapper mapper)
        {
            _convenioService = convenioService;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="convenio"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarConvenio([FromBody] ConvenioRequest convenio)
        {
            try
            {
                var convenioExist = await _convenioService.SearchByNome(convenio.Nome);
                if (convenioExist.Count > 0) { return BadRequest($"Já existe convênio criado com o nome {convenio.Nome}!"); }

                var convenioDTO = _mapper.Map<Convenio>(convenio);
                var convenioCreated = await _convenioService.Create(convenioDTO);

                return Ok(new BaseResponse
                {
                    Message = "Convênio cadastrado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = convenioCreated
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
        [Authorize(Roles = nameof(Constants.Administrador))]
        [HttpDelete("remover")]
        public async Task<ActionResult> RemoverConvenio(long id)
        {
            try
            {
                await _convenioService.Remove(id);

                return Ok(new BaseResponse
                {
                    Message = "Convênio removido com sucesso!",
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
        /// <param name="convenio"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "AdminOrAtendente")]
        [HttpPut("atualizar")]
        public async Task<ActionResult> AtualizarConvenio([FromBody] ConvenioUpdateRequest convenio)
        {
            try
            {
                var convenioExist = await _convenioService.SearchByNome(convenio.Nome);
                if (convenioExist.Count < 0) { return BadRequest($"Convênio {convenio.Nome}, não localizado!"); }

                var Convenio = new Convenio { Nome = convenio.NovoNome, Id = convenioExist[0].Id };

                var convenioAtualizado = await _convenioService.Update(Convenio);

                return Ok(new BaseResponse
                {
                    Message = "Convênio atualizado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = convenioAtualizado
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
        [Authorize]
        [HttpGet("obtertodos")]
        public async Task<ActionResult> ObterTodosConvenios()
        {
            try
            {
                var conveniosLocalizados = await _convenioService.Get();

                return Ok(new BaseResponse
                {
                    Message = "Busca por convênios realizada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = conveniosLocalizados
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
        /// <param name="convenio"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<ActionResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("obterpornome")]
        public async Task<ActionResult> ObterConveniosPorNome([FromBody] ConvenioRequest convenio)
        {
            try
            {
                var convenioLocalizado = await _convenioService.SearchByNome(convenio.Nome);

                return Ok(new BaseResponse
                {
                    Message = $"Pesquisa pelo convênio {convenio.Nome}, realizada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = convenioLocalizado
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
