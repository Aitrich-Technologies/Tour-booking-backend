using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Notifications.Interface
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string role, string message);
        Task<IEnumerable<Notification>> GetNotificationsForRoleAsync(string role);
        Task MarkAsReadAsync(Guid id);
    }
}
