using ApplicationWebEvenements.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;



namespace ApplicationWebEvenements
{
    public class ApiClient
    {
        private HttpClient _httpClient;
        private string _url;
        public ApiClient()
        {
            _httpClient = new HttpClient();
            _url = "http://192.168.50.164:23784/api/";
            _httpClient.DefaultRequestHeaders.Add("ApiKey", "c72e11b4-3118-49a7-999a-e9895d94ad5d");
        }

        public Task<string> GetEvenementsRecents()
        {
           return ExecuterGetRequete("Evenement/GetRecent");
        }

        public Utilisateur PostConnexion(Utilisateur utilisateur)
        {
            var UrlSuffix = "Utilisateur/Login";
            var reponse = ExecuterPostRequete(UrlSuffix, utilisateur).Result;
            var utilisateurRecu = JsonSerializer.Deserialize<Utilisateur>(reponse);
            return utilisateurRecu;
        }

        public bool PostEnregistrement(Utilisateur utilisateur)
        {
            var UrlSuffix = "Utilisateur/New";
            var reponse = ExecuterPostRequete(UrlSuffix, utilisateur).Result;

            return reponse == "";
        }

        private async Task<string> ExecuterGetRequete(string urlSuffix)
        {
            var reponse = await _httpClient.GetAsync(_url + urlSuffix);
            var reponseJson = "";
            if (reponse.IsSuccessStatusCode)
            {
                reponseJson = await reponse.Content.ReadAsStringAsync();
            }
            return reponseJson;
        }

        private async Task<string> ExecuterPostRequete(string urlSuffix, object model)
        {
            var reponse = await _httpClient.PostAsJsonAsync(_url + urlSuffix, model);
            if (reponse.IsSuccessStatusCode)
            {
                return await reponse.Content.ReadAsStringAsync();
            }
            else
            {
                return "fail";
            }
        }
    }
}
