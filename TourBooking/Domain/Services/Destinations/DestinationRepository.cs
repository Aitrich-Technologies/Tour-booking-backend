using Domain.Models;
using Domain.Services.Destinations.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Destinations
{
    public class DestinationRepository: IDestinationRepository
    {
        private readonly TourBookingDbContext _context;

        public DestinationRepository(TourBookingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await _context.Destinations.ToListAsync();
        }

        public async Task<Destination?> GetByIdAsync(Guid id)
        {
            return await _context.Destinations.FindAsync(id);
        }

        public async Task AddAsync(Destination destination)
        {
            await _context.Destinations.AddAsync(destination);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Destination destination)
        {
            var existing = await _context.Destinations.FindAsync(destination.Id);
            if (existing == null) return false;

            existing.Name = destination.Name;
            existing.City = destination.City;
            existing.ImageUrl = destination.ImageUrl;

            _context.Destinations.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null) return false;

            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
