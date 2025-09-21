using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Destinations.Interface
{
    public interface IDestinationRepository
    {
        Task<IEnumerable<Destination>> GetAllAsync();
        Task<Destination?> GetByIdAsync(Guid id);
        Task AddAsync(Destination destination);
        Task<bool> UpdateAsync(Destination destination);
        Task<bool> DeleteAsync(Guid id);


    }
}
