using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CustomerEditRequests
{
    public class TourBookingEditRequestRepository : ITourBookingEditRequestRepository
    {
        private readonly TourBookingDbContext _context;
        public TourBookingEditRequestRepository(TourBookingDbContext context)
        {
            _context = context;
        }

        public async Task<TourBookingEditRequest> CreateAsync(TourBookingEditRequest request)
        {
            _context.EditRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<TourBookingEditRequest?> GetByBookingIdAsync(Guid bookingId)
        {
            return await _context.EditRequests
                .FirstOrDefaultAsync(x => x.BookingId == bookingId && x.Status == Enums.EditStatus.Pending);
        }

        public async Task UpdateAsync(TourBookingEditRequest request)
        {
            _context.EditRequests.Update(request);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<TourBookingEditRequest>> GetAllRequests()
        {
            return _context.EditRequests
                 .Where(r => r.Status==Enums.EditStatus.Pending).ToList();
        }
    }

}
