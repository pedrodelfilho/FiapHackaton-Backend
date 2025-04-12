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
        Task<DesativarUsuarioResponse> DesativarUsuario(string email);
        Task<DesativarUsuarioResponse> AtivarUsuario(string email);
        Task<TrocarSenhaResponse> TrocarSenha(TrocarSenhaRequest trocarSenha);
        Task<ResetarSenhaResponse> ResetarSenha(ResetarSenhaRequest resetarSenha0);
        Task<ObterUsuariosResponse> ObterTodosUsuarios();
        Task<ObterUsuariosResponse> ObterUsuario(string crmOuCpf);
        Task<bool> AlterarPerfilUsuario(string email, string role);
    }
}
