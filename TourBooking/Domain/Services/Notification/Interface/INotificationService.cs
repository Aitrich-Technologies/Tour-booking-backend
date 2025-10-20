using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Notification.Interface
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string role, string message);
    }
}
