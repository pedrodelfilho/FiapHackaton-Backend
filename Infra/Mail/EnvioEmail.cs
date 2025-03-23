using Microsoft.Extensions.Logging;

namespace Infra.Mail
{
    public class EnvioEmail : IEnvioEmail
    {
        protected readonly string origem;
        protected readonly string destino;
        protected readonly string[] anexo;
        protected readonly string servidorSmtp;
        protected readonly int portaSmtp;
        protected readonly IEmailMessage emailMessage;
        private readonly string assuntoErro;
        private readonly string mensagemErro;
        private readonly ILogger<EnvioEmail> logger;

        public EnvioEmail(string origem, string destino, string assuntoErro, string mensagemErro, SmtpModel smtp,
            IEmailMessage emailMessage, ILogger<EnvioEmail> logger, string[] anexo)
        {
            this.origem = origem;
            this.destino = destino;
            this.assuntoErro = assuntoErro;
            this.mensagemErro = mensagemErro;
            this.emailMessage = emailMessage;
            this.logger = logger;
            this.servidorSmtp = smtp.Servidor;
            this.portaSmtp = smtp.Porta;
            this.logger = logger;
            this.anexo = anexo;
        }

        public void EnviarEmail(string origem, string destino, string assunto, string mensagem)
        {
            try
            {
                emailMessage.SendEmail(origem, destino, assunto, mensagem, servidorSmtp, portaSmtp);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Erro ao tentar enviar email ");
            }
        }

        public void EnviarEmailAviso(string emailDestino, string assunto, string mensagem)
        {
            EnviarEmail(origem, emailDestino, assunto, mensagem);
        }

        public void EnviarEmailAvisoAnexo(string emailDestino, string assunto, string mensagem, string[] anexos)
        {
            EnviarEmail(origem, emailDestino, assunto, mensagem);
        }

        public void EnviarEmailErro(string emailDestino, string assunto, Exception exception, string msg)
        {
            EnviarEmail(origem, emailDestino, assunto, string.Format(mensagemErro, msg, exception.ToString()));
        }

        public void EnviarEmailErro(string emailDestino, string assunto, string mensagem, long id)
        {
            EnviarEmail(origem, emailDestino, assunto, string.Format(mensagemErro, id, mensagem));
        }
    }
}
