using System.Net.Http;
using System.Threading.Tasks;

namespace ApplicationWebEvenements
{
    public class ApiClient
    {
        private HttpClient _httpClient;
        private string _url;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _url = "http://10.0.0.149:23784/api/";
        }

        public async Task<string> GetEvenementsRecents()
        {
            var reponse = await _httpClient.GetAsync(_url + "Evenement/GetRecent");
            var reponseJson = "";
            if (reponse.IsSuccessStatusCode)
            {
                reponseJson = await reponse.Content.ReadAsStringAsync();
            }
            return reponseJson;
        }
    }
}
