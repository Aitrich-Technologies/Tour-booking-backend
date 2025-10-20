using Domain.Services.Notification.Interface;
using Microsoft.AspNetCore.SignalR;

namespace TourBooking.API.Hubs
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(string role, string message)
        {
            // Send message to all clients in a specific SignalR group
            await _hubContext.Clients.Group(role).SendAsync("ReceiveNotification", message);
        }
    }
}
