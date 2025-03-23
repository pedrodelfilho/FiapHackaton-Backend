using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("SolicitacaoAgendamento")]
    public class SolicitacaoAgendamento : Base
    {
        [Required(ErrorMessage = "O campo Atendente é obrigatório.")]
        [StringLength(450)]
        public string Paciente { get; set; }

        [Required(ErrorMessage = "O campo Arquivo é obrigatório.")]
        [StringLength(450, MinimumLength = 3, ErrorMessage = "O campo Arquivo deve ter entre 3 e 450 caracteres.")]
        public string Arquivo { get; set; }

        [Required(ErrorMessage = "O campo Data é obrigatório.")]
        [DataType(DataType.DateTime, ErrorMessage = "O campo Data deve ser uma data e hora válida.")]
        public DateTime Data { get; set; }

        [NotMapped]
        public string Atendente { get; set; }

        [NotMapped]
        public IFormFile FormFile { get; set; }

        [ForeignKey("Status")]
        [Required(ErrorMessage = "O campo Status é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O campo Status deve ser maior que zero.")]
        public long IdStatus { get; set; }
        public virtual Status Status { get; set; }

        [ForeignKey("Convenio")]
        [Required(ErrorMessage = "O campo Convênio é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "O campo Convênio deve ser maior que zero.")]
        public long IdConvenio { get; set; }
        public virtual Convenio Convenio { get; set; }

        protected SolicitacaoAgendamento() { }

        public SolicitacaoAgendamento(string paciente, Convenio convenio, string arquivo, DateTime data, Status status, string atendente)
        {
            Paciente = paciente;
            Arquivo = arquivo;
            Data = data;
            Status = status;
            Convenio = convenio;
            Atendente = atendente;
        }

        public void SetData(DateTime data)
        {
            Data = data;
        }

        public void SetStatus(long idStatus)
        {
            IdStatus = idStatus;
        }

        public void SetArquivo(string arquivo)
        {
            Arquivo = arquivo.Trim();
        }
    }
}
