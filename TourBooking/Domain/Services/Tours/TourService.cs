

using Domain.Models;
using Domain.Services.Tours.DTO;
using Domain.Services.Tours.Interface;
using TourBooking.Services.Tours.DTO;
using TourBooking.Services.Tours.Interface;
namespace TourBooking.Services.Tours
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repository;
        public TourService(ITourRepository repository)
        { _repository = repository; }
        //public async Task<Tour> CreateTourAsync(TourRegisterDto dto)
        //{
        //    var tour = new Tour
        //    {
        //        Id = Guid.NewGuid(),
        //        TourName = dto.TourName,
        //        TourDescription = dto.TourDescription,
        //        DestinationId = dto.DestinationId,
        //        NoOfNights = dto.NoOfNights,
        //        Price = dto.Price,
        //        DepartureDate = dto.DepartureDate,
        //        ArrivalDate = dto.ArrivalDate,
        //        CustomerId = dto.CustomerId ?? Guid.Empty,

        //        ConsultantId = dto.ConsultantId,
        //        Status = dto.Status
        //    };
        //    return await _repository.AddTourAsync(tour);
        //}

        public async Task<Tour> CreateTourAsync(TourRegisterDto dto)
        {
            var tour = new Tour
            {
                Id = Guid.NewGuid(),
                TourName = dto.TourName,
                TourDescription = dto.TourDescription,
                DestinationId = dto.DestinationId,
                NoOfNights = dto.NoOfNights,
                Price = dto.Price,
                DepartureDate = dto.DepartureDate,
                ArrivalDate = dto.ArrivalDate,

                // Only set when provided
                CustomerId = dto.CustomerId,

                ConsultantId = dto.ConsultantId,
                Status = dto.Status
            };

            return await _repository.AddTourAsync(tour);
        }
        public async Task<Tour?> GetTourByIdAsync(Guid id)
        {
            return await _repository.GetTourByIdAsync(id);
        }
        public async Task<IEnumerable<Tour>> GetAllToursAsync()
        {
            return await _repository.GetAllToursAsync();
        }

        public async Task<bool> DeleteTourAsync(Guid id)
        {
            var tour = await _repository.GetTourByIdAsync(id);
            if (tour == null) return false;

            await _repository.DeleteAsync(tour);
            return true;
        }

        public async Task<Tour?> PatchTourAsync(Guid id, UpdateTourDto dto)
        {
            var tour = await _repository.GetTourByIdAsync(id);
            if (tour == null) return null;

            tour.TourName = dto.TourName ?? tour.TourName;
            tour.TourDescription = dto.TourDescription ?? tour.TourDescription;
            tour.DestinationId = dto.DestinationId ?? tour.DestinationId;
            tour.NoOfNights = dto.NoOfNights ?? tour.NoOfNights;
            tour.Price = dto.Price ?? tour.Price;
            tour.DepartureDate = dto.DepartureDate ?? tour.DepartureDate;
            tour.ArrivalDate = dto.ArrivalDate ?? tour.ArrivalDate;
            tour.CustomerId = dto.CustomerId ?? tour.CustomerId;
            tour.ConsultantId = dto.ConsultantId ?? tour.ConsultantId;
            tour.Status = dto.Status ?? tour.Status;

            await _repository.UpdateTourAsync(tour);
            return tour;
        }

        public async Task<Tour?> PutTourAsync(Guid id, TourPutDto dto)
        {
            var tour = await _repository.GetTourByIdAsync(id);
            if (tour == null) return null;

            tour.TourName = dto.TourName;
            tour.TourDescription = dto.TourDescription;
            tour.DestinationId = dto.DestinationId;
            tour.NoOfNights = dto.NoOfNights;
            tour.Price = dto.Price;
            tour.DepartureDate = dto.DepartureDate.HasValue
                ? DateOnly.FromDateTime(dto.DepartureDate.Value)
                : tour.DepartureDate;
            tour.ArrivalDate = dto.ArrivalDate.HasValue
                ? DateOnly.FromDateTime(dto.ArrivalDate.Value)
                : tour.ArrivalDate;
            tour.CustomerId = dto.CustomerId;
            tour.ConsultantId = dto.ConsultantId;
            tour.Status = dto.Status;

            return await _repository.UpdateTourAsync(tour);
        }


        public Task<Tour?> UpdateTourAsync(Guid id, UpdateTourDto dto)
        {
            throw new NotImplementedException();
        }




        public async Task<AuthUser?> GetAuthUserByIdAsync(Guid id)
        {
            return await _repository.GetAuthUserByIdAsync(id);
        }

    }
}


