using Domain.Services.Tour.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Tour.Interface
{
    public interface ITourService
    {
        Task<List<TourDto>> GetAllToursAsync(Guid? destinationId = null, string? status = null, DateOnly? departureDate = null);
        Task<TourDto?> GetTourByIdAsync(Guid tourId);
        Task<List<TourDto>> GetToursByCustomerAsync(Guid customerId);
        Task<TourDto> CreateTourAsync(TourDto tourDto);
        Task UpdateTourAsync(TourDto tourDto);
        Task UpdateTourStatusAsync(Guid tourId, UpdateTourStatusDto statusDto);
        Task DeleteTourAsync(Guid tourId);
    }
}
