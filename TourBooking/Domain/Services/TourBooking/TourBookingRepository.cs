using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services.TourBooking.Interface;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services.TourBooking
{
   public class TourBookingRepository:ITourBookingRepository
    {
       
        private readonly TourBookingDbContext _context;
        public TourBookingRepository(TourBookingDbContext context) 
        {
            _context = context;
        }
        public async Task<TourBookingForm> AddTourBookingAsync(TourBookingForm form)
        {
            _context.TourBookingForms.Add(form);
            await _context.SaveChangesAsync();
            return form;
        }
      
       
        public async Task<IEnumerable<TourBookingForm>> GetAllAsync()
        {


            return await _context.TourBookingForms
                          .Include(tb => tb.Tour) // 👈 include related Tour
                          .AsNoTracking()
                          .ToListAsync();
        }

        public async Task<TourBookingForm?> GetTourBookingByIdAsync(Guid id)
        {
            return await _context.TourBookingForms
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<TourBookingForm>> GetTourBookingsByTourIdAsync(Guid tourId)
            => await _context.TourBookingForms
                             .Where(x => x.TourId == tourId)
                             .ToListAsync();


       
        public async Task<TourBookingForm?> UpdateAsync(TourBookingForm booking)
        {
            _context.TourBookingForms.Update(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<bool> DeleteTourBookingAsync(Guid id)
        {
            var entity = await _context.TourBookingForms.FindAsync(id);
            if (entity == null) return false;

            _context.TourBookingForms.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<TourBookingForm>> GetTourBookingsByUserIdAsync(Guid userId)
    => await _context.TourBookingForms
                     .Where(x => x.UserId == userId)
                     .Include(x => x.Tour)
                     .AsNoTracking()
                     .ToListAsync();

    }
}

