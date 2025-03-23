using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TiposExame")]
    public class TiposExame : Base
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo Nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        public TiposExame() { }

        public TiposExame(long id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
