namespace Domain.Entities.Response
{
    public class DesativarUsuarioResponse
    {
        public bool Sucesso => Erros.Count == 0;
        public List<string> Erros { get; private set; }
        public string Message { get; set; }

        public DesativarUsuarioResponse() =>
            Erros = new List<string>();

        public void AdicionarErro(string erro) =>
            Erros.Add(erro);

        public void AdicionarErros(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
