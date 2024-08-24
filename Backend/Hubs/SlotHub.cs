using Backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs
{
    public class SlotHub : Hub
    {
        public async Task SendMessage(List<Slot> message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }

}