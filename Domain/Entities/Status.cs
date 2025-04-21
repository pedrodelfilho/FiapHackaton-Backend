using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Status")]
    public class Status : Base
    {
        [Required]
        [MaxLength(100)]
        public string DsStatus { get; set; }

        public ICollection<Consulta> Consultas { get; set; } = [];
    }
}