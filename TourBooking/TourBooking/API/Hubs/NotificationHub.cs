using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace TourBooking.API.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var role = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(role))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, role);
                Console.WriteLine($"User with role '{role}' joined group '{role}'");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var role = Context.User?.FindFirst(ClaimTypes.Role)?.Value;

            if (!string.IsNullOrEmpty(role))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, role);
                Console.WriteLine($"User with role '{role}' left group '{role}'");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
