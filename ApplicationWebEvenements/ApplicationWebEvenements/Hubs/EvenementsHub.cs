using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task RafraichirEvenements()
        {
            var evenements = await _client.GetEvenementsRecents();
            if (evenements.Count == 0)
            {
                await Clients.All.SendAsync("actionRafraichir", "Erreur de connexion");
            }
            else
            {
                var listeJson = JsonSerializer.Serialize(evenements);
                if (listeJson.Length != listePrésente.Length)
                {
                    listePrésente = listeJson;
                    await Clients.All.SendAsync("actionRafraichir", listeJson);
                }
                await Clients.All.SendAsync("actionRafraichir", "");
            }
        }
    }
}
