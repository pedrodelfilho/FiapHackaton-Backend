using Domain.Entities.Enums;

namespace Domain.Entities.Request
{
    public class AtualizarStatusConsultaRequest
    {
        public long IdConsulta { get; set; }
        public StatusConsulta StatusConsulta { get; set; }
    }
}
