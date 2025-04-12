using Api.Controllers.Shared;
using Application.Resource;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers.v1
{
    public class MedicoController : ApiControllerBase
    {
        private readonly IMedicoService _medicoService;
        private readonly IMapper _mapper;

        public MedicoController(IMedicoService mediicoService, IMapper mapper)
        {
            _medicoService = mediicoService;
            _mapper = mapper;
        }


        /// <summary>
        /// Comando para obter todas especialidades medicas
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ObterUsuariosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("obterespecialidades")]
        public async Task<ActionResult<ObterUsuariosResponse>> ObterEspecialidades()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _medicoService.ObterTodasEspecialidades();
            if (resultado.Success)
                return Ok(resultado);
            return BadRequest();
        }

        /// <summary>
        /// Comando para obter todas especialidades medicas
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ObterUsuariosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Constants.Medico))]
        [HttpGet("obterdisponibilidade")]
        public async Task<ActionResult<ObterUsuariosResponse>> ObterDisponibilidades(string email)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _medicoService.ObterDisponibilidade(email);
            if (resultado.Success)
                return Ok(resultado);
            return BadRequest();
        }

        /// <summary>
        /// Comando para obter todas especialidades medicas
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ObterUsuariosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Constants.Medico))]
        [HttpPost("adicionardisponibilidade")]
        public async Task<ActionResult<ObterUsuariosResponse>> AdicionarDisponibilidade(DisponibilidadeMedicoRequest disponibilidade)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var novaDisponibilidade = _mapper.Map<DisponibilidadeMedico>(disponibilidade);

            var resultado = await _medicoService.AdicionarDisponibilidade(novaDisponibilidade, disponibilidade.Email);
            if (resultado.Success)
                return Ok(resultado);
            else if (resultado.Errors.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, null, errors: resultado.Errors);
                return BadRequest(problemDetails);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Comando para obter todas especialidades medicas
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ObterUsuariosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Constants.Medico))]
        [HttpDelete("removerdisponibilidade")]
        public async Task<ActionResult<ObterUsuariosResponse>> RemoverDisponibilidade(long idDisponibilidade)
        {
            var resultado = await _medicoService.RemoverDisponibilidade(idDisponibilidade);
            if (resultado.Success)
                return Ok(resultado);
            else if (resultado.Errors.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, null, errors: resultado.Errors);
                return BadRequest(problemDetails);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
