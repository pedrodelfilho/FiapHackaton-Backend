using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class ConvenioUpdateRequest
    {
        [Required(ErrorMessage = "O nome para o convênio não pode ser nulo.")]
        [MinLength(3, ErrorMessage = "O nome para o convênio deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome para o convênio deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O nome para o convênio não pode ser nulo.")]
        [MinLength(3, ErrorMessage = "O nome para o convênio deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome para o convênio deve ter no máximo 100 caracteres.")]
        public string NovoNome { get; set; }
    }
}
