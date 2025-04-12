using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DisponibilidadeMedico : Base
    {
        [Required]
        public string MedicoId { get; set; }

        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public bool Ativo { get; set; } = true;

        [ForeignKey("MedicoId")]
        public UserIdentity Medico { get; set; }
    }
}
