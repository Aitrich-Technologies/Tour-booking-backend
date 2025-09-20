using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;




namespace Domain.Services.Terms.Interface
{
    public interface ITermsAndConditionRepository
    {
      
        Task<IEnumerable<TermsAndCondition>> GetTermsByTourIdAsync(Guid tourId);
        Task<TermsAndCondition?> GetTermByIdAsync(Guid id);
        Task AddTermAsync(TermsAndCondition term);
        Task UpdateTermAsync(TermsAndCondition term);
        Task DeleteTermAsync(TermsAndCondition term);
        Task SaveChangesAsync();
        
    }
}
