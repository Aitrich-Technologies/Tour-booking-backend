using Domain.Enums;
using Domain.Models;
using Domain.Services.Tour.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Tour
{

    
        public class TourRepository : ITourRepository
        {
            private readonly TourBookingDbContext _context;

            public TourRepository(TourBookingDbContext context)
            {
                _context = context;
            }

            public async Task<List<Tourss>> GetAllAsync(Guid? destinationId = null, string? status = null, DateOnly? departureDate = null)
            {
                var query = _context.Tours.AsQueryable();

                if (destinationId.HasValue) query = query.Where(t => t.DestinationId == destinationId);
                if (!string.IsNullOrEmpty(status)) query = query.Where(t => t.Status.ToString() == status);
                if (departureDate.HasValue) query = query.Where(t => t.DepartureDate == departureDate);

                return await query
                    .Include(t => t.Destination)
                    .Include(t => t.Customer)
                    .Include(t => t.Consultant)
                    .ToListAsync();
            }

            public async Task<Tourss?> GetByIdAsync(Guid tourId) =>
                await _context.Tours
                    .Include(t => t.Destination)
                    .Include(t => t.Customer)
                    .Include(t => t.Consultant)
                    .FirstOrDefaultAsync(t => t.Id == tourId);

            public async Task<List<Tourss>> GetByCustomerAsync(Guid customerId) =>
                await _context.Tours
                    .Where(t => t.CustomerId == customerId)
                    .Include(t => t.Destination)
                    .Include(t => t.Consultant)
                    .ToListAsync();

            public async Task<Tourss> AddAsync(Tourss tour)
            {
                _context.Tours.Add(tour);
                await _context.SaveChangesAsync();
                return tour;
            }

            public async Task UpdateAsync(Tourss tour)
            {
                _context.Tours.Update(tour);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateStatusAsync(Guid tourId, string status)
            {
                var tour = await _context.Tours.FindAsync(tourId);
                if (tour != null)
                {
                tour.Status = Enum.Parse<TourStatus>(status,true);
       

                await _context.SaveChangesAsync();
                }
            }


            public async Task DeleteAsync(Guid tourId)
            {
                var tour = await _context.Tours.FindAsync(tourId);
                if (tour != null)
                {
                    _context.Tours.Remove(tour);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }


