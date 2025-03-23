using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class HistoricoAgendamentoRequest
    {
        [Required(ErrorMessage = "O id da solicitação não pode ser nulo.")]
        public long IdSolicitacao { get; set; }

        [Required(ErrorMessage = "O status não pode ser nulo.")]
        public long IdStatus { get; set; }

        [Required(ErrorMessage = "A data é obrigatório.")]
        public DateTime Data { get; set; }
    }
}
