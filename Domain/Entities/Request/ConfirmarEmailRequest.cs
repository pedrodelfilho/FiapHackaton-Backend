using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class ConfirmarEmailRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Token { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UserId { get; set; }
    }
}
