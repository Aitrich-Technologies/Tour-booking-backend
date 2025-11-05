using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services.TourBooking.DTO;

namespace Domain.Services.TourBooking.Interface
{
   public interface ITourBookingService
    {
        Task<TourBookingDto>AddTourBookingAsync(TourBookingDto dto);
        Task<IEnumerable<GetBookingDto>> GetAllTourBookingsAsync();
        Task<GetBookingDto?> GetTourBookingByIdAsync(Guid id);
        Task<IEnumerable<GetBookingDto>> GetTourBookingsByTourIdAsync(Guid tourId);
        Task<TourBookingDto?> UpdateTourBookingAsync(Guid id, UpdateTourBookingDto dto);
        Task<GetBookingDto?> PatchTourBookingAsync(Guid id, PatchTourBookingDto dto);
        Task<bool> DeleteTourBookingAsync(Guid id);
        Task<IEnumerable<GetBookingDto>> GetTourBookingsByUserIdAsync(Guid userId);
    }
}
