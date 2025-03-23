namespace Infra.Mail
{
    public class SmtpModel
    {
        public string Servidor { get; set; }
        public int Porta { get; set; }

        public SmtpModel(string servidor, int porta)
        {
            Servidor = servidor;
            Porta = porta;
        }
    }
}
