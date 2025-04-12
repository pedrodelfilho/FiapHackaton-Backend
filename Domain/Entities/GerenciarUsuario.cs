namespace Domain.Entities
{
    public class GerenciarUsuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Bloqueado { get; set; }
    }
}
