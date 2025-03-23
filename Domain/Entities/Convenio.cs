using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Convenio")]
    public class Convenio : Base
    {
        [Required(ErrorMessage = "O campo Conveênio é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Convênio deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        [NotMapped]
        public virtual ICollection<SolicitacaoAgendamento> Solicitacoes { get; set; }

        public Convenio() { }

        public Convenio(string nome, ICollection<SolicitacaoAgendamento> solicitacoes)
        {
            Nome = nome;
            Solicitacoes = solicitacoes;
        }
    }
}
