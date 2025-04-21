namespace Domain.Entities.Request
{
    public class EmailRequest
    {
        public string EmailDestino1 { get; set; }
        public string EmailDestino2 { get; set; }
        public string Assunto { get; set; }
        public string Body { get; set; }
    }
}
