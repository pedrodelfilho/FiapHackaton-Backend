using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class TiposExameUpdateRequest
    {
        [Required(ErrorMessage = "O nome para o exame não pode ser nulo.")]
        [MinLength(3, ErrorMessage = "O nome para o exame deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome para o exame deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O nome para o exame não pode ser nulo.")]
        [MinLength(3, ErrorMessage = "O nome para o exame deve ter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome para o exame deve ter no máximo 100 caracteres.")]
        public string NovoNome { get; set; }
    }
}
