namespace Domain.Entities.Response
{
    public class TrocarSenhaResponse
    {
        public bool Sucesso { get; private set; }
        public List<string> Erros { get; private set; }
        public string Message { get; set; }

        public TrocarSenhaResponse() =>
            Erros = new List<string>();

        public TrocarSenhaResponse(bool sucesso = true) : this() =>
            Sucesso = sucesso;

        public void AdicionarErros(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
