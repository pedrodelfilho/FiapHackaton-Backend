using Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class SolicitacaoConsultaRequest
    {
        [Required]
        public string EmailPaciente { get; set; }
        [Required]
        public string EmailMedico { get; set; }
        [Required]
        public long IdDisponibilidade { get; set; }
        [Required]
        public StatusConsulta StatusConsulta { get; set; }
    }
}
