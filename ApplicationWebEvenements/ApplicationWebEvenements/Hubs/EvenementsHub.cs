using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApplicationWebEvenements.Hubs
{
    public class EvenementsHub : Hub
    {
        private readonly ApiClient _client = new ApiClient();
        private static string reponseListe = "";

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("actionRafraichir", reponseListe);
        }

        public async Task RafraichirEvenements()
        {
            var message = await _client.GetEvenementsRecents();
            if (message.Length != reponseListe.Length)
            {
                reponseListe = message;
                await Clients.All.SendAsync("actionRafraichir", message);
            }
            await Clients.All.SendAsync("actionRafraichir", "");
        }
    }
}
