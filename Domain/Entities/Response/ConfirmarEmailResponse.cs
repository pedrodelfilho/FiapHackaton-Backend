namespace Domain.Entities.Response
{
    public class ConfirmarEmailResponse
    {
        public bool Sucesso { get; private set; }
        public List<string> Erros { get; private set; }
        public string Message { get; set; }

        public ConfirmarEmailResponse() =>
            Erros = new List<string>();

        public ConfirmarEmailResponse(bool sucesso = true) : this() =>
            Sucesso = sucesso;

        public void AdicionarErros(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
