﻿using ApplicationWebEvenements.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace ApplicationWebEvenements
{
    public class ApiClient
    {
        private HttpClient _httpClient;
        private string _url;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _url = "http://192.168.50.164:23784/";
            _httpClient.DefaultRequestHeaders.Add("ApiKey", "c72e11b4-3118-49a7-999a-e9895d94ad5d");
        }

        public async Task<List<Evenement>> GetEvenementsRecents()
        {
            var reponse = await _httpClient.GetAsync(_url + "api/Evenement/GetRecent");
            List<Evenement> evenements = new List<Evenement>();
            if (reponse.IsSuccessStatusCode)
            {
                var reponseJson = await reponse.Content.ReadAsStringAsync();
                evenements = JsonConvert.DeserializeObject<List<Evenement>>(reponseJson);
                foreach (Evenement e in evenements)
                {
                    e.Organisateur = await GetUtilisateurParId(e.IdOrganisateur);
                    e.LienImage = GetImageEvenement(e.IdEvenement);
                }
            }
            return evenements;
        }

        public async Task<List<Evenement>> GetEvenementsParOrganisateur(int id)
        {
            var reponse = await _httpClient.GetAsync(_url + "api/Evenement/GetParOrganisateur/"+id);
            List<Evenement> evenements = new List<Evenement>();
            if (reponse.IsSuccessStatusCode)
            {
                var reponseJson = await reponse.Content.ReadAsStringAsync();
                evenements = JsonConvert.DeserializeObject<List<Evenement>>(reponseJson);
                foreach (Evenement e in evenements)
                {
                    e.Organisateur = await GetUtilisateurParId(e.IdOrganisateur);
                    e.LienImage = GetImageEvenement(e.IdEvenement);
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
                evenement = JsonConvert.DeserializeObject<Evenement>(reponseJson);
                evenement.Organisateur = await GetUtilisateurParId(evenement.IdOrganisateur);
                evenement.LienImage = GetImageEvenement(evenement.IdEvenement);
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
                utilisateur = JsonConvert.DeserializeObject<Utilisateur>(reponseJson);
                utilisateur.LienImage = GetImageUtilisateur(utilisateur.IdUtilisateur);
            }
            else
            {
                utilisateur = null;
            }
            return utilisateur;
        }

        public async Task<Utilisateur> LoginUtilisateur(Utilisateur utilisateur)
        {
            var settings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };
            var userJson = JsonConvert.SerializeObject(utilisateur,settings);
            var contenu = new StringContent(userJson, Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PostAsync(_url + "api/Utilisateur/Login", contenu);
            if (reponse.IsSuccessStatusCode)
            {
                var reponseJson = await reponse.Content.ReadAsStringAsync();
                utilisateur = JsonConvert.DeserializeObject<Utilisateur>(reponseJson);
                if (utilisateur.IdUtilisateur != 0)
                {
                    return utilisateur;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public string GetImageUtilisateur(int id)
        {
            return _url + "images/utilisateurs/"+id+".jpg";
        }

        public string GetImageEvenement(int id)
        {
            return _url + "images/evenements/"+id+".jpg";
        }
    }
}
