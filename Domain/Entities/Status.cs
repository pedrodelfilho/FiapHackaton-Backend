using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Status")]
    public class Status : Base
    {
        [Required(ErrorMessage = "O campo Status é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Status deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        [NotMapped]
        public virtual ICollection<SolicitacaoAgendamento> Solicitacoes { get; set; }
        [NotMapped]
        public virtual ICollection<Agendamento> Agendamentos { get; set; }
        [NotMapped]
        public virtual ICollection<HistoricoAgendamento> HistoricoAgendamentos { get; set; }

        public Status() { }
        public Status(string nome, ICollection<SolicitacaoAgendamento> solicitacoes, ICollection<Agendamento> agendamentos, ICollection<HistoricoAgendamento> historicoAgendamentos)
        {
            Nome = nome;
            Solicitacoes = solicitacoes;
            Agendamentos = agendamentos;
            HistoricoAgendamentos = historicoAgendamentos;
        }
    }
}