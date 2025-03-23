using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class DesativarUsuarioRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UserId { get; set; }
    }
}
