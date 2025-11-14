using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Services.CustomerEditRequests.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CustomerEditRequests
{
    public class TourBookingEditRequestService : ITourBookingEditRequestService
    {
        private readonly ITourBookingEditRequestRepository _repository;
        private readonly IMapper _mapper;
        private readonly TourBookingDbContext _context;

        public TourBookingEditRequestService(
            ITourBookingEditRequestRepository repository,
            IMapper mapper,
            TourBookingDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<TourBookingEditRequestDto>> GetPendingRequestsAsync()
        {
            var requests = await _context.EditRequests
    .Include(x => x.Booking)
        .ThenInclude(b => b.User)
    .Include(x => x.Booking)
        .ThenInclude(b => b.Tour)
    .Where(x => x.Status == EditStatus.Pending)
    .ToListAsync();

            return _mapper.Map<IEnumerable<TourBookingEditRequestDto>>(requests);
        }
    }
}
