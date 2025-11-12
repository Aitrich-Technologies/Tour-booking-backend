using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Participant.Interface
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<ParticipantInformation>> GetParticipantsAsync(Guid bookingId);
        Task<ParticipantInformation?> GetParticipantByIdAsync(Guid id);
        Task AddParticipantAsync(ParticipantInformation participant);
        Task UpdateParticipantAsync(ParticipantInformation participant);
        Task DeleteParticipantAsync(ParticipantInformation participant);
        Task SaveChangesAsync();
    }

}
