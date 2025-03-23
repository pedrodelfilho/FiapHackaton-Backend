namespace Infra.Mail
{
    public interface IEnvioEmail
    {
        void EnviarEmailAviso(string emailDestino, string assunto, string mensagem);
        void EnviarEmailAvisoAnexo(string emailDestino, string assunto, string mensagem, string[] anexos);
        void EnviarEmailErro(string emailDestino, string assunto, Exception exception, string mensagem);
        void EnviarEmailErro(string emailDestino, string assunto, string mensagem, long id);
    }
}
