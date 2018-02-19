using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace LASI.WebService.Services
{
    /// <summary>
    /// Example from https://github.com/aspnet/SignalR/blob/dev/samples/JwtSample/Broadcaster.cs
    /// </summary>
    /// <remarks>https://github.com/aspnet/SignalR/blob/dev/LICENSE.txt</remarks>
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class JwtBroadcaster : Hub
    {
        public async Task BroadcastAsync(string sender, string message)
        {
            await Clients.All.SendAsync("Message", sender, message);
        }
    }
}