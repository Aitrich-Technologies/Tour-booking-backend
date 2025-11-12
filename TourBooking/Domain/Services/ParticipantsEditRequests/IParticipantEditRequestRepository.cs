using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.ParticipantsEditRequests
{
    public interface IParticipantEditRequestRepository
    {
        Task AddAsync(ParticipantEditRequest request);
        Task<IEnumerable<ParticipantEditRequest>> GetPendingRequestsAsync();
        Task<ParticipantEditRequest?> GetByIdAsync(Guid id);
        Task UpdateAsync(ParticipantEditRequest request);
        Task SaveChangesAsync();
    }

}
