using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Especialidade : Base
    {
        [Required]
        [MaxLength(100)]
        public string DsEspecialidade { get; set; }
    }

}
