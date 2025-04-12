namespace Application.Resource
{
    public class Constants
    {
        public const string Administrador = nameof(Administrador);
        public const string Paciente = nameof(Paciente);
        public const string Medico = nameof(Medico);
        public const long SOLICITACAO_ATIVA = 1;
        public const long SOLICITACAO_AUTORIZADO = 2;
        public const long SOLICITACAO_NEGADO = 3;
        public const long AGENDAMENTO_ATIVO = 4;
        public const long AGENDAMENTO_FINALIZADO = 5;
        public const long AGENDAMENTO_CANCELADO_PELO_ATENDENTE = 6;
        public const long AGENDAMENTO_CANCELADO_PELO_PACIENTE = 7;
        public const long AGENDAMENTO_CANCELADO_PELO_ADMIN = 8;
        public const string CONTAINER_GUIA_SOLICITACAO = "guiasolicitacao";
        public const string CONTAINER_RESULTADO_EXAME = "resultado/";
        public const string CONTAINER_PERFIL_USUARIO = "fotoperfil/";
        public const string RESOURCE_RECUPERAR_SENHA = "/autenticacao/recoverPassword";
        public const string RESOURCE_CADASTRO = "/autenticacao/confirmaremail";
    }
}
