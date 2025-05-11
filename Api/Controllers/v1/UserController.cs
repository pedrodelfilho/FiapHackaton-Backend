using Api.Controllers.Shared;
using Domain.Interfaces.Services;
using Application.Resource;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using Entities.Request;

namespace Api.Controllers.v1
{
    public class UserController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="identityService"></param>
        public UserController(IIdentityService identityService) =>
            _identityService = identityService;


        /// <summary>
        /// Comando para cadastrar novos usuários
        /// </summary>
        /// <param name="usuarioCadastro"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(UsuarioCadastroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastrar(UsuarioCadastroRequest usuarioCadastro)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.CadastrarUsuario(usuarioCadastro);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros);
                return BadRequest(problemDetails);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Comando que confirma email
        /// </summary>
        /// <param name="confirmarEmail"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ConfirmarEmailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("ConfirmarEmail")]
        public async Task<IActionResult> ConfirmarEmail(ConfirmarEmailRequest confirmarEmail)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.ConfirmarEmail(confirmarEmail);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros);
                return BadRequest(problemDetails);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Comando que realizar login
        /// </summary>
        /// <param name="usuarioLogin"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(UsuarioLoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioCadastroResponse>> Login(UsuarioLoginRequest usuarioLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.Login(usuarioLogin);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros);
                return Unauthorized(problemDetails);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Comando que cria a requisição para reset de senha
        /// </summary>
        /// <param name="usuarioRecuperarSenha"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(UsuarioRecuperarSenhaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("esquecisenha")]
        public async Task<ActionResult<UsuarioRecuperarSenhaResponse>> EsqueciSenha(UsuarioRecuperarSenhaRequest usuarioRecuperarSenha)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.EsqueciSenha(usuarioRecuperarSenha);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros);
                return BadRequest(problemDetails);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Comando que desativa usuário
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(DesativarUsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Constants.Administrador))]
        [HttpPut("desativarusuario")]
        public async Task<ActionResult<DesativarUsuarioResponse>> DesativarUsuario(DesativarUsuarioRequest email)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.DesativarUsuario(email.Email);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros);
                return Unauthorized(problemDetails);
            }

            return Unauthorized();

        }

        /// <summary>
        /// Comando que desativa usuário
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(DesativarUsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Constants.Administrador))]
        [HttpPut("ativarusuario")]
        public async Task<ActionResult<DesativarUsuarioResponse>> AtivarUsuario(DesativarUsuarioRequest email)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.AtivarUsuario(email.Email);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros);
                return Unauthorized(problemDetails);
            }

            return Unauthorized();

        }

        /// <summary>
        /// Comando para alterar perfil
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = nameof(Constants.Administrador))]
        [HttpPut("alterarfuncaousuario")]
        public async Task<ActionResult> AlterarPerfilUsuario(AlterarPerfilUsuarioRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.AlterarPerfilUsuario(model.Email, model.Role);

            if (resultado)
                return Ok(resultado);

            return BadRequest();
        }

        /// <summary>
        /// Comando que realiza a troca da senha
        /// </summary>
        /// <param name="trocarSenha"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(TrocarSenhaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Paciente")]
        [HttpPost("trocarsenha")]
        public async Task<ActionResult<TrocarSenhaResponse>> TrocarSenha(TrocarSenhaRequest trocarSenha)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.TrocarSenha(trocarSenha);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros);
                return BadRequest(problemDetails);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Comando para realizar o logout
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.Logout();
            if (resultado)
                return Ok(resultado);

            return BadRequest();
        }

        /// <summary>
        /// Comando que reseta a senha
        /// </summary>
        /// <param name="resetarSenha"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("resetarsenha")]
        public async Task<ActionResult<ResetarSenhaResponse>> ResetarSenha(ResetarSenhaRequest resetarSenha)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.ResetarSenha(resetarSenha);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros);
                return Unauthorized(problemDetails);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Comando para obter todos os usuários cadastrados 
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ObterUsuariosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        //[Authorize]
        [HttpGet("obterusuarios")]
        public async Task<ActionResult<ObterUsuariosResponse>> ObterTodosUsuarios()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.ObterTodosUsuarios();
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, Request, errors: resultado.Erros);
                return Unauthorized(problemDetails);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Comando para obter todos os usuários cadastrados 
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ObterUsuariosResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("obterusuario")]
        public async Task<ActionResult<ObterUsuariosResponse>> ObterUsuario(string crmOuCpf)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var resultado = await _identityService.ObterUsuario(crmOuCpf);
            if (resultado.Sucesso)
                return Ok(resultado);
            else if (resultado.Erros.Count > 0)
            {
                var problemDetails = new CustomProblemDetails(HttpStatusCode.BadRequest, crmOuCpf, errors: resultado.Erros);
                return Unauthorized(problemDetails);
            }

            return Unauthorized();
        }
    }
}
