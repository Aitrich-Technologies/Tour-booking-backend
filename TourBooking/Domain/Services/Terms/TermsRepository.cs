using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services.Terms.Interface;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services.Terms
{
    public class TermsRepository:ITermsAndConditionRepository
    {
        private readonly TourBookingDbContext _context;

        public TermsRepository(TourBookingDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TermsAndCondition>> GetTermsByTourIdAsync(Guid tourId)
        {
            return await _context.TermsAndConditions
                                 .Where(t => t.TourId == tourId)   // 
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<TermsAndCondition?> GetTermByIdAsync(Guid id)
        {
            return await _context.TermsAndConditions.FindAsync(id);
        }

        public async Task AddTermAsync(TermsAndCondition term)
        {
            await _context.TermsAndConditions.AddAsync(term);
        }

        public async Task UpdateTermAsync(TermsAndCondition term)
        {
            _context.TermsAndConditions.Update(term);

        }

        public async Task DeleteTermAsync(TermsAndCondition term)
        {
            _context.TermsAndConditions.Remove(term);

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

