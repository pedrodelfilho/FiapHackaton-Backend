using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("HistoricoAgendamento")]
    public class HistoricoAgendamento : Base
    {
        
        [Required(ErrorMessage = "O campo Data é obrigatório.")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo Data deve ser uma data e hora válida.")]
        public DateTime Data { get; set; }

        public string Atendente { get; set; } = null!;

        [ForeignKey("Status")]
        [Required(ErrorMessage = "O campo Status é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O campo Status deve ser maior que zero.")]
        public long IdStatus { get; set; }
        public virtual Status Status { get; set; } = null!;

        [ForeignKey("SolicitacaoAgendamento")]
        [Required(ErrorMessage = "O campo Solicitação é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O campo Solicitação deve ser maior que zero.")]
        public long IdSolicitacao { get; set; }
        public virtual SolicitacaoAgendamento Solicitacao { get; set; }

        public HistoricoAgendamento() { }

        public HistoricoAgendamento(SolicitacaoAgendamento solicitacao, Status status, DateTime data, long idSolicitacao, long idStatus, string atendente)
        {
            Solicitacao = solicitacao;
            Status = status;
            Data = data;
            IdSolicitacao = idSolicitacao;
            IdStatus = idStatus;
            Atendente = atendente;
        }
    
        public void SetStatus(long idStatus)
        {
            IdStatus = idStatus;
        }
    }
}
