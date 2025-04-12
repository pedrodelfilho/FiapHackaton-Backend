using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class AlterarPerfilUsuarioRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Role { get; set; }
    }
}
