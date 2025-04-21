using Domain.Entities;
using System.Web;

namespace Application.Services.Handler
{
    public class Handler
    {
        public static string Deploy(string idUsuario, string token, string resource)
        {
            var uriBuilder = new UriBuilder("https", "localhost", 44300);
            uriBuilder.Path = resource;
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["userid"] = idUsuario;
            uriBuilder.Query = query.ToString();
            var urlString = uriBuilder.ToString();
            return urlString;
        }
    }
}
