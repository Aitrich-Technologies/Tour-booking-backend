using Domain.Models;
using Domain.Services.Notifications.Interface;
using Microsoft.EntityFrameworkCore;
using System;

public class NotificationRepository : INotificationRepository
{
    private readonly TourBookingDbContext _context;

    public NotificationRepository(TourBookingDbContext context)
    {
        _context = context;
    }

    public async Task<Notification> AddAsync(Notification notification)
    {
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
        return notification;
    }

    public async Task<List<Notification>> GetByRoleAsync(string role)
    {
        return await _context.Notifications
            .Where(n => n.Role == role)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task MarkAsReadAsync(Guid id)
    {
        var n = await _context.Notifications.FindAsync(id);
        if (n != null)
        {
            n.IsRead = true;
            await _context.SaveChangesAsync();
        }
    }
}
