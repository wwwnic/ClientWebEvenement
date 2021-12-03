using Microsoft.AspNetCore.SignalR;
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

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("actionRafraichir", listePrésente);
        }

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

        public async Task RafraichirCommentaires()
        {
            //TODO
        }

        public async Task RafraichirParticipants()
        {
            //TODO
        }
    }
}
