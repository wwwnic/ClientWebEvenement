using ApplicationWebEvenements.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

namespace ApplicationWebEvenements
{
    public class ApiClient
    {
        private HttpClient _httpClient;
        private string _url;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _url = "http://10.0.0.149:23784/";
            _httpClient.DefaultRequestHeaders.Add("ApiKey", "c72e11b4-3118-49a7-999a-e9895d94ad5d");
        }

        public async Task<string> GetEvenementsRecentsEnJson()
        {
            var reponse = await _httpClient.GetAsync(_url + "api/Evenement/GetRecent");
            var reponseJson = await reponse.Content.ReadAsStringAsync();
            return reponseJson;
        }

        public async Task<List<Evenement>> GetEvenementsRecents()
        {
            var reponse = await _httpClient.GetAsync(_url + "api/Evenement/GetRecent");
            List<Evenement> evenements = new List<Evenement>();
            if (reponse.IsSuccessStatusCode)
            {
                var reponseJson = await reponse.Content.ReadAsStringAsync();
                evenements = JsonSerializer.Deserialize<List<Evenement>>(reponseJson);
                foreach (Evenement e in evenements)
                {
                    //e.IdOrganisateur = await GetUtilisateurParId(e.IdOrganisateur);
                    //e.LienImage = GetImageEvenement(e.IdEvenement);
                }
            }
            return evenements;
        }

        public async Task<Evenement> GetEvenementParId(int idEvenement)
        {
            var reponse = await _httpClient.GetAsync(_url + "api/Evenement/GetById?id=" + idEvenement);
            Evenement evenement;
            if (reponse.IsSuccessStatusCode)
            {
                var reponseJson = await reponse.Content.ReadAsStringAsync();
                evenement = JsonSerializer.Deserialize<Evenement>(reponseJson);
                //evenement.IdOrganisateur = await GetUtilisateurParId(evenement.IdOrganisateur);
                //evenement.LienImage = GetImageEvenement(evenement.IdEvenement);
            }
            else
            {
                evenement = null;
            }
            return evenement;
        }

        public async Task<Utilisateur> GetUtilisateurParId(int idUtilisateur)
        {
            var reponse = await _httpClient.GetAsync(_url + "api/Utilisateur/GetById?id=" + idUtilisateur);
            Utilisateur utilisateur;
            if (reponse.IsSuccessStatusCode)
            {
                var reponseJson = await reponse.Content.ReadAsStringAsync();
                utilisateur = JsonSerializer.Deserialize<Utilisateur>(reponseJson);
                //utilisateur.LienImage = GetImageUtilisateur(utilisateur.IdUtilisateur);
            }
            else
            {
                utilisateur = null;
            }
            return utilisateur;
        }

        public string GetImageUtilisateur(int id)
        {
            return _url + "images/utilisateurs/" + id + ".jpg";
        }

        public string GetImageEvenement(int id)
        {
            return _url + "images/evenements/" + id + ".jpg";
        }
    }
}
