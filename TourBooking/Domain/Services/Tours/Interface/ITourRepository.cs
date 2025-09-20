using Domain.Models;
namespace Domain.Services.Tours.Interface
{
    public interface ITourRepository
    {
        Task<Tour> AddTourAsync(Tour tour);
        Task<Tour?> GetTourByIdAsync(Guid id);
        Task<IEnumerable<Tour>> GetAllToursAsync();
        //Task<Tour?> UpdateTourAsync(Guid id, Tour updatedTour);
        Task<Tour> TourPutAsync(Tour tour);
        Task<Tour?> UpdateTourAsync(Tour tour);
        Task DeleteAsync(Tour tour);
        //Task<IEnumerable<Tour>> GetToursByStatusAsync(string status);
    }
}

