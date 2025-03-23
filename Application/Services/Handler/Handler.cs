using Domain.Entities;
using System.Web;

namespace Application.Services.Handler
{
    public class Handler
    {
        public static string Deploy(string idUsuario, string token, string resource)
        {
            var uriBuilder = new UriBuilder("https", "localhost", 7097);
            uriBuilder.Path = resource;
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["userid"] = idUsuario;
            uriBuilder.Query = query.ToString();
            var urlString = uriBuilder.ToString();
            return urlString;
        }
        public static HistoricoAgendamento BuildHistoricoAgendamento(SolicitacaoAgendamento solicitacaoAgendamento)
        {
            return new HistoricoAgendamento
            {
                IdSolicitacao = solicitacaoAgendamento.Id,
                IdStatus = solicitacaoAgendamento.IdStatus,
                Data = DateTime.Now,
                Atendente = solicitacaoAgendamento.Atendente
            };
        }

        public static HistoricoAgendamento BuildHistoricoAgendamento(Agendamento agendamento, long idStatus)
        {
            return new HistoricoAgendamento
            {
                IdSolicitacao = agendamento.IdSolicitacao,
                IdStatus = idStatus,
                Data = DateTime.Now,
                Atendente = agendamento.Atendente
            };
        }
    }
}
