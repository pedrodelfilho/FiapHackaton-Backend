using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class DisponibilidadeMedicoRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public TimeSpan HoraInicio { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public TimeSpan HoraFim { get; set; }
    }
}
