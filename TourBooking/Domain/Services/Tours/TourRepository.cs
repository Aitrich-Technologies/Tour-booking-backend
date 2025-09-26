using Domain.Models;
using Domain.Services.Tours.Interface;
using Microsoft.EntityFrameworkCore;

namespace TourBooking.Services.Tours
{
    public class TourRepository : ITourRepository
    {
        private readonly TourBookingDbContext _context;

        public TourRepository(TourBookingDbContext context)
        {
            _context = context;
        }

        public async Task<Tour> AddTourAsync(Tour tour)
        {
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();
            return tour;
        }

        public async Task<Tour?> GetTourByIdAsync(Guid id)
        {
            return await _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.Customer)
                .Include(t => t.Consultant)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tour>> GetAllToursAsync()
        {
            return await _context.Tours
                .Include(t => t.Destination)
                .Include(t => t.Customer)
                .Include(t => t.Consultant)
                .ToListAsync();
        }

        public async Task DeleteAsync(Tour tour)
        {
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
        }

        public async Task<Tour> UpdateTourAsync(Tour tour)
        {
            _context.Tours.Update(tour);
            await _context.SaveChangesAsync();
            return tour;
        }

        public async Task<Tour> TourPutAsync(Tour tour)
        {
            _context.Tours.Update(tour);
            await _context.SaveChangesAsync();
            return tour;
        }

        public async Task<AuthUser?> GetAuthUserByIdAsync(Guid id)
        {
            return await _context.AuthUsers
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
