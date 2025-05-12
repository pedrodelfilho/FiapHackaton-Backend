using Domain.Entities;
using System.Web;

namespace Application.Services.Handler
{
    public class Handler
    {
        public static string Deploy(string idUsuario, string token, string resource)
        {
            var uriBuilder = new UriBuilder("https://frontendd-g2checd7czhghpf5.brazilsouth-01.azurewebsites.net/");
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
