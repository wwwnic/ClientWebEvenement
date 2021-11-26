using Microsoft.AspNetCore.SignalR;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApplicationWebEvenements.Hubs
{
    public class EvenementsHub : Hub
    {
        private readonly ApiClient _client;

        public EvenementsHub(ApiClient client)
        {
            _client = client;
        }

        public async Task RefreshEvenements(string message)
        {
            //var message = await _client.GetAllEvenements();
            await Clients.All.SendAsync("refreshEvenements", message);
        }
    }
}
