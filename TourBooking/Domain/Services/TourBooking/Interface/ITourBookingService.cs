﻿using System;
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
        Task<IEnumerable<TourBookingDto>> GetAllTourBookingsAsync();
        Task<TourBookingDto?> GetTourBookingByIdAsync(Guid id);
        Task<IEnumerable<TourBookingDto>> GetTourBookingsByTourIdAsync(Guid tourId);
        Task<TourBookingDto?> UpdateTourBookingAsync(Guid id, UpdateTourBookingDto dto);
        Task<TourBookingDto?> PatchTourBookingAsync(Guid id, PatchTourBookingDto dto);
        Task<bool> DeleteTourBookingAsync(Guid id);
    }
}
