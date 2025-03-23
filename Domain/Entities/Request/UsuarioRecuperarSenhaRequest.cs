using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class UsuarioRecuperarSenhaRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
        public string Email { get; set; }
    }
}
