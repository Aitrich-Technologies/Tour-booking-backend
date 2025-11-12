using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.ParticipantsEditRequests
{
    public class ParticipantEditRequestRepository : IParticipantEditRequestRepository
    {
        private readonly TourBookingDbContext _context;

        public ParticipantEditRequestRepository(TourBookingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ParticipantEditRequest request)
        {
            await _context.ParticipantEditRequests.AddAsync(request);
        }

        public async Task<IEnumerable<ParticipantEditRequest>> GetPendingRequestsAsync()
        {
            return await _context.ParticipantEditRequests
                .Where(r => r.Status == "PENDING")
                .ToListAsync();
        }

        public async Task<ParticipantEditRequest?> GetByIdAsync(Guid id)
        {
            return await _context.ParticipantEditRequests.FindAsync(id);
        }

        public async Task UpdateAsync(ParticipantEditRequest request)
        {
            _context.ParticipantEditRequests.Update(request);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
