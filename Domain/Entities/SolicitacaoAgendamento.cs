using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Consulta")]
    public class Consulta : Base
    {
        [Required]
        public string IdUsuarioPaciente { get; set; }

        [Required]
        public string IdUsuarioMedico { get; set; }

        [Required]
        public long IdDisponibilidade { get; set; }

        [Required]
        public long IdStatus { get; set; }

        [Required]
        public DateTime DataSolicitacao { get; set; }


        public static Consulta FactoryConsulta(string idPaciente, string idMedico, long idDisponibilidade, long idStatus)
        {
            return new Consulta
            {
                IdUsuarioPaciente = idPaciente,
                IdUsuarioMedico = idMedico,
                IdDisponibilidade = idDisponibilidade,
                IdStatus = idStatus,
                DataSolicitacao = DateTime.Now,
            };
        }
    }


}
