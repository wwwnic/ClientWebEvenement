using ApplicationWebEvenements.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
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
            _url = "http://192.168.0.107:44312/api/";
        }

        public Task<string> GetEvenementsRecents()
        {
            return ExecuterGetRequete("Evenement/GetRecent");
        }

        public string PostConnexion(Utilisateur utilisateur)
        {
            var UrlSuffix = "Utilisateur/Login";
            var reponse = ExecuterPostRequete(UrlSuffix, utilisateur).Result;
            return reponse;
        }

        public string getEvenementParID(int id)
        {
            var UrlSuffix = "Evenement/GetById/" + id;
            var reponse = ExecuterGetRequete(UrlSuffix).Result;
            return reponse;
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
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PostAsync(_url + urlSuffix, stringContent);
            if (reponse.IsSuccessStatusCode)
            {
                return await reponse.Content.ReadAsStringAsync();
            }
            else
            {
                return "";
            }
        }
    }
}
