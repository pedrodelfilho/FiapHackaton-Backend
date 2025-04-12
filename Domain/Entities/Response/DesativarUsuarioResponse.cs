namespace Domain.Entities.Response
{
    public class DesativarUsuarioResponse
    {
        public bool Sucesso { get; private set; }
        public List<string> Erros { get; private set; }
        public string Message { get; set; }

        public DesativarUsuarioResponse() =>
            Erros = new List<string>();

        public DesativarUsuarioResponse(bool sucesso = true) : this() =>
            Sucesso = sucesso;

        public void AdicionarErro(string erro) =>
            Erros.Add(erro);

        public void AdicionarErros(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
