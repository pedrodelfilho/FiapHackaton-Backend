namespace Domain.Entities.Response
{
    public class ObterUsuariosResponse
    {
        public bool Sucesso { get; private set; }
        public List<string> Erros { get; private set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public ObterUsuariosResponse() =>
            Erros = new List<string>();

        public ObterUsuariosResponse(bool sucesso = true) : this() =>
            Sucesso = sucesso;

        public void AdicionarErros(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
