using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Tour.Interface
{
    public interface ITourRepository
    {
        Task<List<Tourss>> GetAllAsync(Guid? destinationId = null, string? status = null, DateOnly? departureDate = null);
        Task<Tourss?> GetByIdAsync(Guid tourId);
        Task<List<Tourss>> GetByCustomerAsync(Guid customerId);
        Task<Tourss> AddAsync(Tourss tour);
        Task UpdateAsync(Tourss tour);
        Task UpdateStatusAsync(Guid tourId, string status);
        Task DeleteAsync(Guid tourId);
    }
}
