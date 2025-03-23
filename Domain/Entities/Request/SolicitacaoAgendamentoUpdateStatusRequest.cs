using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class SolicitacaoAgendamentoUpdateStatusRequest
    {
        [Required(ErrorMessage = "A Solicitação é obrigatório.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        public long IdStatus { get; set; }

        public string Atendente { get; set; }

        public List<long> Exames { get; set; }
    }
}
