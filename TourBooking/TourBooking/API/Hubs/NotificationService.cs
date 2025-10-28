using Domain.Models;
using Domain.Services.Notifications.Interface;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TourBooking.API.Hubs
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(INotificationRepository repo, IHubContext<NotificationHub> hub)
        {
            _repo = repo;
            _hub = hub;
        }

        public async Task SendNotificationAsync(string role, string message)
        {
            // ✅ 1. Save to database
            var notification = new Notification
            {
                Role = role,
                Message = message
            };
            await _repo.AddAsync(notification);

            // ✅ 2. Send live message via SignalR
            await _hub.Clients.Group(role).SendAsync("ReceiveNotification", message);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsForRoleAsync(string role)
        {
            return await _repo.GetByRoleAsync(role);
        }

        public async Task MarkAsReadAsync(Guid id)
        {
            await _repo.MarkAsReadAsync(id);
        }
    }
}
