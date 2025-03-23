using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Request
{
    public class SolicitacaoAgendamentoRequest
    {
        [Required(ErrorMessage = "O id do usuário paciente é obrigatório.")]
        public string Paciente { get; set; }

        [Required(ErrorMessage = "Se não for por convênio, selecione 'Nenhum convênio'.")]
        public long IdConvenio { get; set; }

        [Required(ErrorMessage = "O arquivo é obrigatório")]
        public IFormFile FormFile { get; set; }

        public string Atendente { get; set; }
    }
}
