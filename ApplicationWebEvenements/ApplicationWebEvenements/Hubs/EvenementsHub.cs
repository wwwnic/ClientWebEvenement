﻿using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ApplicationWebEvenements.Models;

namespace ApplicationWebEvenements.Hubs
{
    public class EvenementsHub : Hub
    {
        private readonly ApiClient _client = new ApiClient();
        private static string listePrésente = "";
        public static int idEvenementDetails = 0;

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("actionRafraichir", listePrésente);
        }

        /// <summary>
        /// Rafraichit automatiquement la liste d'événements récents
        /// </summary>
        public async Task RafraichirEvenementsRecents()
        {
            var evenements = await _client.GetEvenementsRecents();
            if (evenements.Count == 0)
            {
                await Clients.All.SendAsync("actionRafraichir", "Erreur de connexion");
            }
            else
            {
                foreach (Evenement e in evenements)
                {
                    e.SetDateFormatéePourJS();
                }
                var listeJson = JsonConvert.SerializeObject(evenements);
                if (listeJson.Length != listePrésente.Length)
                {
                    listePrésente = listeJson;
                    await Clients.All.SendAsync("actionRafraichir", listeJson);
                }
                await Clients.All.SendAsync("actionRafraichir", "");
            }
        }

        /// <summary>
        /// Rafraichit automatiquement la liste de participants et de commentaires sur la page de détails
        /// d'un événement
        /// </summary>
        public async Task RafraichirDetails()
        {
            var commentaires = await _client.GetCommentairesParEvenement(idEvenementDetails);
            var participants = await _client.GetUtilisateurParEvenement(idEvenementDetails);
            if (participants.Count == 0)
            {
                await Clients.All.SendAsync("rafraichirParticipants", "Erreur de connexion");
            } 
            else
            {
                var listeJsonCommentaires = JsonConvert.SerializeObject(commentaires);
                var listeJsonParticipants = JsonConvert.SerializeObject(participants);
                await Clients.All.SendAsync("rafraichirParticipants", listeJsonParticipants);
                await Clients.All.SendAsync("rafraichirCommentaires", listeJsonCommentaires);
            }
        }

        /// <summary>
        /// Modifie la participation de l'utilisateur à un événement quand il appuie sur le bouton
        /// </summary>
        /// <param name="estParticipant">Détermine si l'utilisateur est présentement un participant ou non</param>
        public async Task ModifierParticipation(bool estParticipant)
        {
            var utilisateurEvenement = new Utilisateurevenement
            {
                IdEvenement = idEvenementDetails,
                IdUtilisateur = (int)Context.GetHttpContext().Session.GetInt32("login")
            };
            bool reponse;
            if (estParticipant)
            {
                reponse = await _client.DeleteParticipation(utilisateurEvenement);
            }
            else
            {
                reponse = await _client.AddParticipation(utilisateurEvenement);
            }
            if (!reponse)
            {
                await Clients.All.SendAsync("erreurParticipation");
            }
        }
    }
}
