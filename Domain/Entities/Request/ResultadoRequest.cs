using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class ResultadoRequest
    {
        [Required(ErrorMessage = "O id da solicitação não pode ser nulo.")]
        public long IdSolicitacao { get; set; }

        [Required(ErrorMessage = "O arquivo é obrigatório")]
        public string Arquivo { get; set; }

        [Required(ErrorMessage = "A data é obrigatório.")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O atendente é obrigatório")]
        public string Atendente { get; set; }
    }
}
