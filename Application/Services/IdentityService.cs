using Application.Resource;
using Domain.Entities;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Domain.Interfaces.Services;
using Entities.Request;
using Infra.Data;
using Infra.Mail;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<UserIdentity> _signInManager;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IEnvioEmail _emailMessage;

        public IdentityService(SignInManager<UserIdentity> signInManager,
                               UserManager<UserIdentity> userManager,
                               IOptions<JwtOptions> jwtOptions,
                               IEnvioEmail emailMessage)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
            _emailMessage = emailMessage;
        }

        public async Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro)
        {
            var identityUser = new UserIdentity
            {
                UserName = usuarioCadastro.Email,
                Email = usuarioCadastro.Email,
                NomeCompleto = usuarioCadastro.Nome
            };

            var result = await _userManager.CreateAsync(identityUser, usuarioCadastro.Senha);

            if (result.Succeeded)
            {
                var userFromDb = await _userManager.FindByNameAsync(identityUser.UserName);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);

                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                string url = Handler.Handler.Deploy(userFromDb.Id, token, Constants.RESOURCE_CADASTRO);

                _emailMessage.EnviarEmailAviso(userFromDb.Email, $"Confirmação de cadastro.", url);
            }

            var usuarioCadastroResponse = new UsuarioCadastroResponse(result.Succeeded);
            if (!result.Succeeded && result.Errors.Any())
            {
                string descricaoAmigavelErro = await TratarErroResponse(null, identityUser.Email, result);
                usuarioCadastroResponse.AdicionarErros(new List<string> { descricaoAmigavelErro });
            }
            else if (result.Succeeded)
            {
                usuarioCadastroResponse.Message = $"Link de confirmação do cadastro enviado para o email: {usuarioCadastro.Email}";
            }

            return usuarioCadastroResponse;
        }

        public async Task<ConfirmarEmailResponse> ConfirmarEmail(ConfirmarEmailRequest confirmarEmail)
        {
            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmarEmail.Token));

            var user = await _userManager.FindByIdAsync(confirmarEmail.UserId);

            var result = await _userManager.ConfirmEmailAsync(user, token);

            var confirmarEmailResponse = new ConfirmarEmailResponse(result.Succeeded);
            if (!result.Succeeded)
            {
                string descricaoAmigavelErro = await TratarErroResponse(null, user.Email, result);
                confirmarEmailResponse.AdicionarErros(new List<string> { descricaoAmigavelErro });
            }
            else
                confirmarEmailResponse.Message = "Confirmação de e-mail realizada com sucesso.";

            return confirmarEmailResponse;
        }

        public async Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);
            if (result.Succeeded)
                return await GerarCredenciais(usuarioLogin.Email);

            var usuarioLoginResponse = new UsuarioLoginResponse();
            if (!result.Succeeded)
            {
                var descricaoAmigavelErro = await TratarErroResponse(result, usuarioLogin.Email, null);
                usuarioLoginResponse.AdicionarErro(descricaoAmigavelErro.ToString());
            }

            return usuarioLoginResponse;
        }

        public async Task<ResetarSenhaResponse> ResetarSenha(ResetarSenhaRequest resetarSenha)
        {
            var user = await _userManager.FindByIdAsync(resetarSenha.Id);
            var resetarSenhaResponse = new ResetarSenhaResponse(user != null);

            if (resetarSenhaResponse.Erros.Count > 0 || user == null)
            {
                resetarSenhaResponse.AdicionarErro(await TratarErroResponse(null, user.Email, null));
                return resetarSenhaResponse;
            }

            var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetarSenha.Token));

            var result = await _userManager.ResetPasswordAsync(user, token, resetarSenha.NovaSenha);

            if (!result.Succeeded)
            {
                var descricaoAmigavelErro = await TratarErroResponse(null, user.Email, result);
                resetarSenhaResponse.AdicionarErro(descricaoAmigavelErro.ToString());
            }

            return resetarSenhaResponse;
        }

        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<UsuarioRecuperarSenhaResponse> EsqueciSenha(UsuarioRecuperarSenhaRequest usuarioRecuperarSenhaRequest)
        {
            var result = await _userManager.FindByEmailAsync(usuarioRecuperarSenhaRequest.Email);
            var usuarioRecuperarSenhaResponse = new UsuarioRecuperarSenhaResponse();
            if (result == null)
                usuarioRecuperarSenhaResponse.AdicionarErro(await TratarErroResponse(null, usuarioRecuperarSenhaRequest.Email, null));

            if (usuarioRecuperarSenhaResponse.Erros.Count > 0)
                return usuarioRecuperarSenhaResponse;

            var token = await _userManager.GeneratePasswordResetTokenAsync(result);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            string url = Handler.Handler.Deploy(result.Id, token, Constants.RESOURCE_RECUPERAR_SENHA);

            _emailMessage.EnviarEmailAviso(result.Email, "Acesse o link para resetar sua senha: ", url);

            return usuarioRecuperarSenhaResponse;
        }

        public async Task<DesativarUsuarioResponse> DesativarUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var desativarUsuarioResponse = new DesativarUsuarioResponse();

            if (desativarUsuarioResponse.Sucesso)
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            else
                desativarUsuarioResponse.AdicionarErro(await TratarErroResponse(null, user.Email, null));

            return desativarUsuarioResponse;
        }

        public async Task<bool> AlterarPerfilUsuario(string idUsuario, string role)
        {
            var user = await _userManager.FindByIdAsync(idUsuario);
            try
            {
                switch (role)
                {
                    case Constants.Admin:
                        await _userManager.AddToRoleAsync(user, Constants.Admin);
                        break;
                    case Constants.Atendente:
                        await _userManager.AddToRoleAsync(user, Constants.Atendente);
                        break;
                    case Constants.Paciente:
                        await _userManager.AddToRoleAsync(user, Constants.Paciente);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(role), "Role inválida.");
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ObterUsuariosResponse> ObterTodosUsuarios()
        {
            var users = await _userManager.Users.ToListAsync();
            var obterTodosUsuariosResponse = new ObterUsuariosResponse(users != null)
            {
                Message = "Busca por todos os usuários realizada com sucesso!",
                Data = users ?? []
            };

            return obterTodosUsuariosResponse;
        }

        public async Task<TrocarSenhaResponse> TrocarSenha(TrocarSenhaRequest trocarSenha)
        {
            var user = await _userManager.FindByEmailAsync(trocarSenha.Email);
            var trocarSenhaResponse = new TrocarSenhaResponse(user != null);

            if (!trocarSenhaResponse.Sucesso)
            {
                string descricaoAmigavelErro = await TratarErroResponse(null, trocarSenha.Email, null);
                trocarSenhaResponse.AdicionarErros(new List<string> { descricaoAmigavelErro });
                return trocarSenhaResponse;
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, trocarSenha.SenhaAntiga, trocarSenha.NovaSenha);
            if (!changePasswordResult.Succeeded)
            {
                string descricaoAmigavelErro = await TratarErroResponse(null, trocarSenha.Email, changePasswordResult);
                trocarSenhaResponse.AdicionarErros(new List<string> { descricaoAmigavelErro });
                return trocarSenhaResponse;
            }

            trocarSenhaResponse.Message = "Senha alterada com sucesso, por favor refaça o login.";

            await _signInManager.RefreshSignInAsync(user);
            return trocarSenhaResponse;
        }

        private async Task<string> TratarErroResponse(SignInResult result, string email, IdentityResult identityResult)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (identityResult != null && identityResult.Errors.Any())
                return identityResult.Errors.First().Description.ToString();
            else if (user == null)
                return "Essa conta está bloqueada";
            else if (!await _signInManager.UserManager.IsEmailConfirmedAsync(user))
                return "Confirmação por email pendente.";
            else if (result != null && result.IsLockedOut)
                return "Essa conta está bloqueada";
            else if (result != null && result.IsNotAllowed)
                return "Essa conta não tem permissão para fazer login";
            else if (result != null && result.RequiresTwoFactor)
                return "É necessário confirmar o login no seu segundo fator de autenticação";
            else
                return "Usuário e/ou senha inválido!";
        }

        private async Task<UsuarioLoginResponse> GerarCredenciais(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var accessTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: true);
            var refreshTokenClaims = await ObterClaims(user, adicionarClaimsUsuario: false);

            var dataExpiracaoAccessToken = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
            var dataExpiracaoRefreshToken = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

            var accessToken = GerarToken(accessTokenClaims, dataExpiracaoAccessToken);
            var refreshToken = GerarToken(refreshTokenClaims, dataExpiracaoRefreshToken);

            return new UsuarioLoginResponse
            (
                sucesso: true,
                accessToken: accessToken,
                refreshToken: refreshToken
            );
        }

        private string GerarToken(IEnumerable<Claim> claims, DateTime dataExpiracao)
        {
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: dataExpiracao,
                signingCredentials: _jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<IList<Claim>> ObterClaims(UserIdentity user, bool adicionarClaimsUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString())
            };

            if (adicionarClaimsUsuario)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(userClaims);

                foreach (var role in roles)
                    claims.Add(new Claim("role", role));
            }

            return claims;
        }

    }
}
