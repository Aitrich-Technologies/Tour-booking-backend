using AutoMapper;
using Domain.Models;
using Domain.Services.Tour.DTO;
using Domain.Services.Tour.Interface;
using Domain.Services.TourBooking.DTO;
using Domain.Services.TourBooking.Interface;

namespace Domain.Services.TourBooking
{
    public class TourBookingService : ITourBookingService
    {
        private readonly ITourBookingRepository _repository;
        private readonly ITourRepository _tourRepository;
        private readonly IMapper _mapper;

        public TourBookingService(ITourBookingRepository repository, IMapper mapper,ITourRepository tourRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _tourRepository = tourRepository;
        }

        public async Task<TourBookingDto> AddTourBookingAsync(TourBookingDto dto)
        {
          


                var entity = _mapper.Map<TourBookingForm>(dto);
                entity.Id = Guid.NewGuid(); // ensure new ID


                var saved = await _repository.AddTourBookingAsync(entity);
                return _mapper.Map<TourBookingDto>(saved);
            
        }

        public async Task<IEnumerable<TourBookingDto>> GetAllTourBookingsAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TourBookingDto>>(entities);
        }

        public async Task<TourBookingDto?> GetTourBookingByIdAsync(Guid id)
        {
            var entity = await _repository.GetTourBookingByIdAsync(id);
            return entity == null ? null : _mapper.Map<TourBookingDto>(entity);
        }

        public async Task<IEnumerable<TourBookingDto>> GetTourBookingsByTourIdAsync(Guid tourId)
        {
            var entities = await _repository.GetTourBookingsByTourIdAsync(tourId);
            return _mapper.Map<IEnumerable<TourBookingDto>>(entities);
        }

        public async Task<TourBookingDto?> UpdateTourBookingAsync(Guid id, UpdateTourBookingDto dto)
        {
            var entity = await _repository.GetTourBookingByIdAsync(id);
            if (entity == null) return null;

            // AutoMapper maps UpdateTourBookingDto → existing entity
            _mapper.Map(dto, entity);

            var saved = await _repository.UpdateAsync(entity);
            return _mapper.Map<TourBookingDto>(saved);
        }

        public async Task<TourBookingDto?> PatchTourBookingAsync(Guid id, PatchTourBookingDto dto)
        {
            var entity = await _repository.GetTourBookingByIdAsync(id);
            if (entity == null) return null;

            // For patching, we want to only update non-null values
            _mapper.Map(dto, entity);

            var saved = await _repository.UpdateAsync(entity);
            return _mapper.Map<TourBookingDto>(saved);
        }

        public async Task<bool> DeleteTourBookingAsync(Guid id)
            => await _repository.DeleteTourBookingAsync(id);

        public async Task<IEnumerable<TourBookingDto>> GetTourBookingsByUserIdAsync(Guid userId)
        {
            var entities = await _repository.GetTourBookingsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TourBookingDto>>(entities);
        }

    }
}
