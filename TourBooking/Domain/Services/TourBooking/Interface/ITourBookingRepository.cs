using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services.TourBooking.DTO;

namespace Domain.Services.TourBooking.Interface
{
   public interface ITourBookingRepository
    {
        Task<TourBookingForm> AddTourBookingAsync(TourBookingForm form);
        Task<IEnumerable<TourBookingForm>> GetAllTourBookingsAsync();
        Task<TourBookingForm?> GetTourBookingByIdAsync(Guid id);
        Task<IEnumerable<TourBookingForm>> GetTourBookingsByTourIdAsync(Guid tourId);
        Task<TourBookingForm> PatchTourBookingAsync(TourBookingForm form);


        Task<TourBookingForm> UpdateTourBookingAsync(TourBookingForm form);
        //Task UpdateTourBookingAsync(TourBookingForm booking);
        
    

    Task<bool> DeleteTourBookingAsync(Guid id);
      
    }
}
