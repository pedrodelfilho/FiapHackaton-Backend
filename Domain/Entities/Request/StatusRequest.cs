using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class StatusRequest
    {
        [Required(ErrorMessage = "O nome para o status não pode ser nulo.")]
        [MinLength(3, ErrorMessage = "O nome para o status deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome para o status deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }
    }
}
