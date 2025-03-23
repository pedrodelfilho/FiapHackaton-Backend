using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("SolicitacaoExame")]
    public class SolicitacaoExame : Base
    {
        [Required(ErrorMessage = "O campo Solicitação é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O campo Solicitação deve ser maior que zero.")]
        public long IdSolicitacao { get; set; }

        [Required(ErrorMessage = "O campo Exame é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O campo Exame deve ser maior que zero.")]
        public long IdExame { get; set; }

        public virtual SolicitacaoAgendamento SolicitacaoAgendamento { get; set; }

        [NotMapped]
        public virtual TiposExame TiposExames { get; set; }

        public SolicitacaoExame() { }

        public SolicitacaoExame(long idSolicitacao, long idExame)
        {
            IdSolicitacao = idSolicitacao;
            IdExame = idExame;
        }
    }
}
