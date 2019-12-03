using System;
using System.Threading.Tasks;
using RestSharp;

namespace Web.Services
{
    public static class IdenticonService
    {
        private static IRestClient GetWebClient()
        {
            return new RestClient("https://identicon-1132.appspot.com");
        }
        
        public static async Task<string> RequestInBase64(string hash)
        {
            var request = new RestRequest("{data}", Method.GET);
            request.AddUrlSegment("data", hash);
            request.AddParameter("f", "base64");

            IRestResponse response = await GetWebClient().ExecuteTaskAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Houve um problema gerar uma imagem para seu perfil, por favor tente novamente mais tarde");
            }

            return response.Content;
        }
    }
}