using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Resultado")]
    public class Resultado : Base
    {
        [Required(ErrorMessage = "O campo Solicitação é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O campo Solicitação deve ser maior que zero.")]
        public long IdSolicitacao { get; set; }

        [Required(ErrorMessage = "O campo Arquivo é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O campo Arquivo deve ter entre 3 e 50 caracteres.")]
        public string Arquivo { get; set; }

        [Required(ErrorMessage = "O campo Data é obrigatório.")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo Data deve ser uma data e hora válida.")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O campo Atendente é obrigatório.")]
        [StringLength(450)]
        public string Atendente { get; set; }

        [NotMapped]
        public virtual SolicitacaoAgendamento Solicitacao { get; set; }

        protected Resultado() { }

        public Resultado(SolicitacaoAgendamento solicitacao, string arquivo, DateTime data, string atendente)
        {
            Solicitacao = solicitacao;
            Arquivo = arquivo;
            Data = data;
            Atendente = atendente;
        }
    }
}
