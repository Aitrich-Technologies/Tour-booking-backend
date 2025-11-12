using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CustomerEditRequests
{
    public interface ITourBookingEditRequestRepository
    {
        Task<TourBookingEditRequest> CreateAsync(TourBookingEditRequest request);
        Task<TourBookingEditRequest?> GetByBookingIdAsync(Guid bookingId);
        Task UpdateAsync(TourBookingEditRequest request);
    }

}
