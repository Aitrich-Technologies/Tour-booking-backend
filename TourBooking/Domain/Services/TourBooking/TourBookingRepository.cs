using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
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
            form.ReferenceNumber = GenerateReferenceNumber(form);
            _context.TourBookingForms.Add(form);
            await _context.SaveChangesAsync();
            return form;
        }
      
       
        public async Task<IEnumerable<TourBookingForm>> GetAllAsync()
        {


            return await _context.TourBookingForms
                .Include(tb => tb.User)
                          .Include(tb => tb.Tour).ThenInclude(t => t.Destination)
                          .Include(tb => tb.ParticipantInformations)
                          .AsNoTracking()
                          .ToListAsync();
        }

        public async Task<TourBookingForm?> GetTourBookingByIdAsync(Guid id)
        {
            return await _context.TourBookingForms
                .Include(tb => tb.User) // ✅ Include user info
                .Include(tb => tb.Tour).ThenInclude(t => t.Destination)
                .Include(tb => tb.ParticipantInformations)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<TourBookingForm?> GetByTourBookingByIdAsync(Guid id)
        {
            var bookings = await _context.TourBookingForms.FindAsync(id);
            bookings.IsEditAllowed = false;
            bookings.EditStatusCheck = EditStatus.Pending;
           await _context.SaveChangesAsync();
            return bookings;

        }

        public async Task<IEnumerable<TourBookingForm>> GetTourBookingsByTourIdAsync(Guid tourId)
            => await _context.TourBookingForms
                             .Where(x => x.TourId == tourId)
                            .Include(x => x.Tour).ThenInclude(t => t.Destination)
            .Include(tb => tb.ParticipantInformations)
                            .AsNoTracking()
                            .ToListAsync();



        //public async Task<TourBookingForm?> UpdateAsync(TourBookingForm booking)
        //{
        //    _context.TourBookingForms.Update(booking);
        //    await _context.SaveChangesAsync();
        //    return booking;
        //}
        public async Task<TourBookingForm?> UpdateAsync(TourBookingForm booking)
        {
            _context.TourBookingForms.Attach(booking);
            _context.Entry(booking).State = EntityState.Modified;

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
        {
            var tours = await _context.TourBookingForms
                     .Where(x => x.UserId == userId)
                     .Include(x => x.Tour).ThenInclude(t => t.Destination)
                     .Include(tb => tb.ParticipantInformations)
                     .AsNoTracking()
                     .ToListAsync();
            return tours;
        }
        public async Task<IEnumerable<TourBookingForm>> GetByStatusAsync(BookStatus status)
        {
            return await _context.TourBookingForms
                .Where(b => b.Status == status)
                .Include(b => b.Tour)
                .ToListAsync();
        }
        private string GenerateReferenceNumber(TourBookingForm booking)
        {
            
            string shortTourId = booking.TourId.ToString().Substring(0, 3).ToUpper();
            string randomPart = new Random().Next(1000, 9999).ToString();

            return $"LSC-{shortTourId}-{randomPart}";
        }

    }
}

