using AutoMapper;
using Domain.Models;
using Domain.Services.Tour.DTO;

using Domain.Services.Tour.Interface;


namespace Domain.Services.Tour.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repository;
        private readonly IMapper _mapper;

        public TourService(ITourRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TourDto>> GetAllToursAsync(Guid? destinationId = null, string? status = null, DateOnly? departureDate = null)
        {
            var tours = await _repository.GetAllAsync(destinationId, status, departureDate);
            return _mapper.Map<List<TourDto>>(tours);
        }

        public async Task<TourDto?> GetTourByIdAsync(Guid tourId)
        {
            var tour = await _repository.GetByIdAsync(tourId);
            return _mapper.Map<TourDto?>(tour);
        }

        public async Task<List<TourDto>> GetToursByCustomerAsync(Guid customerId)
        {
            var tours = await _repository.GetByCustomerAsync(customerId);
            return _mapper.Map<List<TourDto>>(tours);
        }

        public async Task<TourDto> CreateTourAsync(TourDto tourDto)
        {
            var tour = _mapper.Map<Tourss>(tourDto);
            var created = await _repository.AddAsync(tour);
            return _mapper.Map<TourDto>(created);
        }

        public async Task UpdateTourAsync(TourDto tourDto)
        {
            var tour = _mapper.Map<Tourss>(tourDto);
            await _repository.UpdateAsync(tour);
        }

        public async Task UpdateTourStatusAsync(Guid tourId, UpdateTourStatusDto statusDto)
        {
            await _repository.UpdateStatusAsync(tourId, statusDto.Status.ToString());
        }

        public async Task DeleteTourAsync(Guid tourId)
        {
            await _repository.DeleteAsync(tourId);
        }

    }
}
