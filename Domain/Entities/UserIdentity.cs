using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserIdentity : IdentityUser
    {
        public string? NomeCompleto { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Crm {  get; set; }
        public string? Cpf { get; set; }
        public long? IdEspecialidade { get; set; }

        [ForeignKey("IdEspecialidade")]
        public Especialidade? Especialidade { get; set; }
        public ICollection<DisponibilidadeMedico> Disponibilidades { get; set; } = [];

    }
}
