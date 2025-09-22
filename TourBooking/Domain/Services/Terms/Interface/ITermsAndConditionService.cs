using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services.Terms.DTO;

namespace Domain.Services.Terms.Interface
{
    


        public interface ITermsAndConditionService
        {
            Task<IEnumerable<TermsDto>> GetTermsByTourIdAsync(Guid tourId);
     
            Task<TermsDto?> GetTermByIdAsync(Guid id);
            Task<TermsDto> AddTermAsync(TermsDto term);
            Task<TermsDto> UpdateTermAsync(Guid id,TermsDto term);
            Task DeleteTermAsync(Guid id);


        }



    
}
