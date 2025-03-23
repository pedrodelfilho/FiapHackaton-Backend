using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace Domain.Entities
{
    [Table("Agendamento")]
    public class Agendamento : Base
    {
        [Required(ErrorMessage = "O campo Data é obrigatório.")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo Data deve ser uma data e hora válida.")]

        public DateTime Data { get; set; }

        [NotMapped]
        public virtual string Atendente { get; set; }

        [ForeignKey("SolicitacaoAgendamento")]
        [Required(ErrorMessage = "O campo Solicitação é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O campo Solicitação deve ser maior que zero.")]
        public long IdSolicitacao { get; set; }
        public virtual SolicitacaoAgendamento Solicitacao { get; set; } = null!;

        [ForeignKey("Status")]
        [Required(ErrorMessage = "O campo Status é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O campo Status deve ser maior que zero.")]
        public long IdStatus { get; set; }
        public virtual Status Status { get; set; } = null!;

        protected Agendamento() { }

        public Agendamento(SolicitacaoAgendamento solicitacao, DateTime data, Status status, long idSolicitacao, long idStatus, string atendente)
        {
            Solicitacao = solicitacao;
            Data = data;
            Status = status;
            IdStatus = idStatus;
            IdSolicitacao = idSolicitacao;
            Atendente = atendente;
        }
    }
}
