using Domain.Services.Participant.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Participant.Interface
{
    public interface IParticipantService
    {
        Task<IEnumerable<ParticipantDto>> GetParticipantsAsync(Guid bookingId);
        Task<ParticipantDto?> GetParticipantByIdAsync(Guid id);
        Task<ParticipantDto> AddParticipantAsync(Guid bookingId, ParticipantDto dto);
        Task<ParticipantDto?> UpdateParticipantAsync(Guid id, ParticipantDto dto);
        Task<bool> DeleteParticipantAsync(Guid id);
        Task<bool> RequestParticipantEditAsync(Guid participantId, ParticipantDto dto, Guid requestedBy);
        Task<bool> ApproveEditRequestAsync(Guid requestId, bool approve, string? comments = null);
    }



}
