namespace Domain.Entities.Enums
{
    public enum StatusConsulta
    {
        /// <summary>
        /// Consulta criada, aguardando confirmação do médico.
        /// </summary>
        AguardandoAutorizacao = 1,

        /// <summary>
        /// Consulta foi autorizada pelo médico.
        /// </summary>
        Autorizado = 2,

        /// <summary>
        /// Consulta foi cancelada pelo paciente.
        /// </summary>
        CanceladoPaciente = 3,

        /// <summary>
        /// Consulta foi cancelada pelo médico.
        /// </summary>
        CanceladoMedico = 4,

        /// <summary>
        /// Consulta foi realizada com sucesso.
        /// </summary>
        Realizado = 5
    }

}
