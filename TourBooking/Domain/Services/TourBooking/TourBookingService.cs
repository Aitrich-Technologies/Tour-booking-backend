using AutoMapper;
using Domain.Models;
using Domain.Services.Email.Helper;
using Domain.Services.Email.Interface;
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
        private readonly IMailService _mailService;

        public TourBookingService(
            ITourBookingRepository repository,
            IMapper mapper,
            ITourRepository tourRepository,
            IMailService mailService)
        {
            _repository = repository;
            _mapper = mapper;
            _tourRepository = tourRepository;
            _mailService = mailService;
        }

        public async Task<TourBookingDto> AddTourBookingAsync(TourBookingDto dto)
        {
            var entity = _mapper.Map<TourBookingForm>(dto);
            entity.Id = Guid.NewGuid();

            var saved = await _repository.AddTourBookingAsync(entity);
            var bookings=  await _repository.GetTourBookingByIdAsync(saved.Id);

            var details = _mapper.Map<GetBookingDto>(bookings);

            // ✅ Send confirmation email
            var email = new MailRequest
            {
                ToEmail=details.User.Email,
                Subject = "Tour Booking Confirmation",
                Body = $@"
            <h2>Booking Confirmation</h2>
            <p>Dear {details.FirstName} {details.LastName},</p>
            <p>Your booking for <strong>{details.Tour.TourName}</strong> has been successfully placed!</p>
            <p>Departure: {details.Tour?.DepartureDate?.ToString("dd MMM yyyy")}<br/>
            Arrival: {details.Tour?.ArrivalDate?.ToString("dd MMM yyyy")}</p>
            <p>Thank you for booking with us.</p>"
            };

            await _mailService.SendEmailAsync(email);

            return _mapper.Map<TourBookingDto>(saved);
        }

        public async Task<IEnumerable<GetBookingDto>> GetAllTourBookingsAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetBookingDto>>(entities);
        }

        public async Task<GetBookingDto?> GetTourBookingByIdAsync(Guid id)
        {
            var entity = await _repository.GetTourBookingByIdAsync(id);
            return entity == null ? null : _mapper.Map<GetBookingDto>(entity);
        }

        public async Task<IEnumerable<GetBookingDto>> GetTourBookingsByTourIdAsync(Guid tourId)
        {
            var entities = await _repository.GetTourBookingsByTourIdAsync(tourId);
            return _mapper.Map<IEnumerable<GetBookingDto>>(entities);
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

        public async Task<IEnumerable<GetBookingDto>> GetTourBookingsByUserIdAsync(Guid userId)
        {
            var entities = await _repository.GetTourBookingsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<GetBookingDto>>(entities);
        }

    }
}
