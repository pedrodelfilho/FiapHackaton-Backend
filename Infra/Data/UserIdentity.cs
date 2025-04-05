using Microsoft.AspNetCore.Identity;

namespace Infra.Data
{
    public class UserIdentity : IdentityUser
    {
        public string? NomeCompleto { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
