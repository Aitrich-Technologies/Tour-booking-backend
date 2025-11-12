using Domain.Models;
using Domain.Services.Participant.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Participant
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly TourBookingDbContext _context;

        public ParticipantRepository(TourBookingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParticipantInformation>> GetParticipantsAsync(Guid bookingId)
        {
            return await _context.ParticipantInformations
                .Where(p => p.LeadId == bookingId)
                .ToListAsync();
        }

        public async Task<ParticipantInformation?> GetParticipantByIdAsync(Guid id)
        {
            return await _context.ParticipantInformations
                .FindAsync(id);
        }

        public async Task AddParticipantAsync(ParticipantInformation participant)
        {
            await _context.ParticipantInformations.AddAsync(participant);
        }

        public async Task UpdateParticipantAsync(ParticipantInformation participant)
        {
            _context.ParticipantInformations.Update(participant);
        }

        public async Task DeleteParticipantAsync(ParticipantInformation participant)
        {
            _context.ParticipantInformations.Remove(participant);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }


}
