using Domain.Entities.Request;
using Domain.Entities.Response;
using Entities.Request;

namespace Domain.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro);
        Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin);
        Task<bool> Logout();
        Task<UsuarioRecuperarSenhaResponse> EsqueciSenha(UsuarioRecuperarSenhaRequest usuarioRecuperarSenha);
        Task<ConfirmarEmailResponse> ConfirmarEmail(ConfirmarEmailRequest confirmarEmail);
        Task<DesativarUsuarioResponse> DesativarUsuario(string id);
        Task<TrocarSenhaResponse> TrocarSenha(TrocarSenhaRequest trocarSenha);
        Task<ResetarSenhaResponse> ResetarSenha(ResetarSenhaRequest resetarSenha0);
        Task<ObterUsuariosResponse> ObterTodosUsuarios();
        Task<bool> AlterarPerfilUsuario(string idUsuario, string role);
    }
}
