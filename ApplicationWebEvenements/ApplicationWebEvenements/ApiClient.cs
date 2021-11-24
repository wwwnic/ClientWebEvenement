using ApplicationWebEvenements.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _url = "http://localhost:23784/api/";
        }

        public async Task<IEnumerable<Utilisateur>> GetAllUtilisateurs()
        {
            List<Utilisateur> utilisateurs = new List<Utilisateur>();
            var reponse = await _httpClient.GetAsync(_url + "Utilisateur/GetAll");
            string responseJson = await reponse.Content.ReadAsStringAsync();
            utilisateurs = JsonConvert.DeserializeObject<List<Utilisateur>>(responseJson);
            return utilisateurs;
        }
    }
}
