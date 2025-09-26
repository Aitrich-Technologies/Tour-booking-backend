using Domain.Models;
namespace Domain.Services.Tours.Interface
{
    public interface ITourRepository
    {
        Task<Tour> AddTourAsync(Tour tour);
        Task<Tour?> GetTourByIdAsync(Guid id);
        Task<IEnumerable<Tour>> GetAllToursAsync();
        Task<Tour> TourPutAsync(Tour tour);
        Task<Tour?> UpdateTourAsync(Tour tour);
        Task DeleteAsync(Tour tour);
        Task<AuthUser?> GetAuthUserByIdAsync(Guid id);
        //Task<IEnumerable<Tour>> GetToursByStatusAsync(string status);
    }
}

