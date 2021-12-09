using ApplicationWebEvenements.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace ApplicationWebEvenements
{
    public class ApiClient
    {
        private HttpClient _httpClient;
        private string _url;
        private JsonSerializerSettings _authSetting;

        /// <summary>
        /// Client pour accéder aux méthodes de l'API avec authetification par clé API
        /// </summary>
        public ApiClient()
        {
            _httpClient = new HttpClient();
            _url = "http://140.82.8.101/";
            _httpClient.DefaultRequestHeaders.Add("ApiKey", "c72e11b4-3118-49a7-999a-e9895d94ad5d");
            _authSetting = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

        }

        /// <summary>
        /// Retourne les événements récents
        /// </summary>
        /// <returns>La liste d'événements par date ascendante</returns>
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

        /// <summary>
        /// Retourne les evenements d'un organisateur donné
        /// </summary>
        /// <param name="id">Id de l'organisateur</param>
        /// <returns>La liste d'événements</returns>
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

        /// <summary>
        /// Retourne les evenements auxquels l'utilisateur donné participe
        /// </summary>
        /// <param name="id">Id de l'utilisateur</param>
        /// <returns>La liste d'événement</returns>
        public async Task<List<Evenement>> GetEvenementsParParticipant(int id)
        {
            var reponse = await _httpClient.GetAsync(_url + "api/Evenement/GetParParticipant/" + id);
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

        /// <summary>
        /// Retourne un événement par son ID
        /// </summary>
        /// <param name="idEvenement">Id événement</param>
        /// <returns>L'événement</returns>
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
                evenement.IdEvenement = idEvenement;
            }
            else
            {
               evenement = null;
            }
            return evenement;
        }

        /// <summary>
        /// Retourne la liste d'événements qui correspondent aux mot cles donnés
        /// </summary>
        /// <param name="recherche">Mot-clés de la recherche</param>
        /// <returns>Liste d'événements</returns>
        public async Task<List<Evenement>> GetEvenementsParRecherche(Recherche recherche)
        {
            var mois = recherche.Mois;
            if (mois != null)
            {
                mois = recherche.Mois.Substring(0, 7);
            }
            var query = new Dictionary<string, string>()
            {
                ["nom"] = recherche.Nom,
                ["mois"] = mois,
                ["location"] = recherche.Location,
                ["organisateur"] = recherche.Organisateur
            };
            var urlRequete = QueryHelpers.AddQueryString(_url + "api/Evenement/GetParRecherche", query);
            var reponse = await _httpClient.GetAsync(urlRequete);
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

        /// <summary>
        /// Crée un événement dans la base de données
        /// </summary>
        /// <param name="evenement">L'événement à insérer</param>
        /// <returns>L'événement après son insertion</returns>
        public async Task<Evenement> CreerEvenement(Evenement evenement)
        {
            var evenementJson = JsonConvert.SerializeObject(evenement, _authSetting);
            var contenu = new StringContent(evenementJson, Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PostAsync(_url + "api/Evenement/New", contenu);
            if (reponse.IsSuccessStatusCode)
            {
                var reponseJson = await reponse.Content.ReadAsStringAsync();
                evenement = JsonConvert.DeserializeObject<Evenement>(reponseJson);
                evenement.LienImage = GetImageEvenement(evenement.IdEvenement);
                if (evenement.IdEvenement != 0)
                {
                    return evenement;
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

        /// <summary>
        /// Modifie un événement
        /// </summary>
        /// <param name="evenement">L'événement avec les modifications</param>
        /// <returns>La réponse de la requete</returns>
        public async Task<bool> EditEvenement(Evenement evenement)
        {
            var evenementJson = JsonConvert.SerializeObject(evenement, _authSetting);
            var contenu = new StringContent(evenementJson, Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PutAsync(_url + "api/Evenement/Update", contenu);
            return reponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Retoune un utilisateur par son id
        /// </summary>
        /// <param name="idUtilisateur">L'id recherché</param>
        /// <returns>Un utilisateur</returns>
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

        /// <summary>
        /// Retourne les utilisateurs qui participent à un événement
        /// </summary>
        /// <param name="idEvenement">L'id de l'événement</param>
        /// <returns>La liste d'utilisateurs</returns>
        public async Task<List<Utilisateur>> GetUtilisateurParEvenement(int idEvenement)
        {
            var reponse = await _httpClient.GetAsync(_url + "api/Utilisateur/GetByEvent?idEvenement=" + idEvenement);
            List<Utilisateur> listeUtilisateur = new List<Utilisateur>();

            if (reponse.IsSuccessStatusCode)
            {
                var reponseJson = await reponse.Content.ReadAsStringAsync();
                listeUtilisateur = JsonConvert.DeserializeObject<List<Utilisateur>>(reponseJson);
                foreach(Utilisateur u in listeUtilisateur)
                {
                    u.LienImage = GetImageUtilisateur(u.IdUtilisateur);
                }
            }
            return listeUtilisateur;
        }

        /// <summary>
        /// Ajoute une participation à un événement
        /// </summary>
        /// <param name="ue">L'association utilisateur-événement</param>
        /// <returns>La réponse de la requete</returns>
        public async Task<bool> AddParticipation(Utilisateurevenement ue)
        {
            var json = JsonConvert.SerializeObject(ue, _authSetting);
            var contenu = new StringContent(json, Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PostAsync(_url + "api/Utilisateur/addParticipation", contenu);
            return reponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Retire une participation à un événement
        /// </summary>
        /// <param name="ue">L'association utilisateur-événement</param>
        /// <returns>La réponse de la requete</returns>
        public async Task<bool> DeleteParticipation(Utilisateurevenement ue)
        {
            var json = JsonConvert.SerializeObject(ue, _authSetting);
            var contenu = new StringContent(json, Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PostAsync(_url + "api/Utilisateur/deleteParticipation", contenu);
            return reponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Retourne la liste de commentaires d'un événement
        /// </summary>
        /// <param name="idEvenement">id de l'événement</param>
        /// <returns>La liste de commentaires</returns>
        public async Task<List<Commentaire>> GetCommentairesParEvenement(int idEvenement)
        {
            var reponse = await _httpClient.GetAsync(_url + "api/Commentaire/GetByEvenement?id="+ idEvenement);
            List<Commentaire> commentaires = new List<Commentaire>();
            if (reponse.IsSuccessStatusCode)
            {
                var reponseJson = await reponse.Content.ReadAsStringAsync();
                commentaires = JsonConvert.DeserializeObject<List<Commentaire>>(reponseJson);
                foreach (Commentaire c in commentaires)
                {
                    c.Utilisateur = await GetUtilisateurParId(c.IdUtilisateur);
                    c.SetDateFormatéePourJS();
                }
            }
            return commentaires;
        }

        /// <summary>
        /// Ajoute un commentaire.
        /// </summary>
        /// <param name="commentaire">Commentaire à ajouter (Contient l'id de l'événement)</param>
        /// <returns>La réponse de la requete</returns>
        public async Task<bool> AddCommentaire(Commentaire commentaire)
        {
            var CommentaireJson = JsonConvert.SerializeObject(commentaire, _authSetting);
            var contenu = new StringContent(CommentaireJson, Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PostAsync(_url + "api/Commentaire/New", contenu);
            return reponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Fonction de login via le service
        /// </summary>
        /// <param name="utilisateur">L'utilisateur qui se login</param>
        /// <returns>La réponse de la requete</returns>
        public async Task<Utilisateur> Login(Utilisateur utilisateur)
        {
            var userJson = JsonConvert.SerializeObject(utilisateur,_authSetting);
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

        /// <summary>
        /// Création d'un compte utilisateur
        /// </summary>
        /// <param name="utilisateur">Utilisateur à créer</param>
        /// <returns>La réponse de la requete</returns>
        public async Task<bool> SignUp(Utilisateur utilisateur)
        {
            var userJson = JsonConvert.SerializeObject(utilisateur, _authSetting);
            var contenu = new StringContent(userJson, Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PostAsync(_url + "api/Utilisateur/New", contenu);
            return reponse.IsSuccessStatusCode;
        }

        /// <summary>
        /// Modifier un compte
        /// </summary>
        /// <param name="utilisateur">Utilisateur modifié</param>
        /// <returns>La réponse de la requete</returns>
        public async Task<bool> EditAccount(Utilisateur utilisateur)
        {
            var userJson = JsonConvert.SerializeObject(utilisateur, _authSetting);
            var contenu = new StringContent(userJson, Encoding.UTF8, "application/json");
            var reponse = await _httpClient.PutAsync(_url + "api/Utilisateur/Update", contenu);
            return reponse.IsSuccessStatusCode;
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
