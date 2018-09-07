using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetRadarWebServer.Hubs
{
    public class User {
        string ID;
        int recieve;
        int send;
        string state;
        string network_Interface;
        DateTime last_seen;
    }
    public class RadarHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
