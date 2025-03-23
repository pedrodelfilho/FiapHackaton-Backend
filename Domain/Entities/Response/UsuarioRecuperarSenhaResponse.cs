namespace Domain.Entities.Response
{
    public class UsuarioRecuperarSenhaResponse
    {
        public bool Sucesso => Erros.Count == 0;
        public List<string> Erros { get; private set; }
        public string Message { get; set; }

        public UsuarioRecuperarSenhaResponse() =>
            Erros = new List<string>();

        public void AdicionarErro(string erro) =>
            Erros.Add(erro);

        public void AdicionarErros(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
