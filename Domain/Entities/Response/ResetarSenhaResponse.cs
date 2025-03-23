namespace Domain.Entities.Response
{
    public class ResetarSenhaResponse
    {
        public bool Sucesso { get; private set; }
        public List<string> Erros { get; private set; }
        public string Message { get; set; }

        public ResetarSenhaResponse() =>
            Erros = new List<string>();

        public ResetarSenhaResponse(bool sucesso = true) : this() =>
            Sucesso = sucesso;
        public void AdicionarErro(string erro) =>
            Erros.Add(erro);

        public void AdicionarErros(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
