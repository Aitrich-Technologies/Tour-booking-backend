using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Services.TourBooking.DTO;
using Domain.Services.TourBooking.Interface;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services.TourBooking
{
    public class TourBookingService : ITourBookingService
    {
        private readonly ITourBookingRepository _repository;
        private readonly IMapper _mapper;
        private readonly TourBookingDbContext _context;
        public TourBookingService(ITourBookingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //public async Task<TourBookingDto> AddTourBookingAsync(TourBookingDto dto)
        //{
        //       var details=_mapper.Map<TourBookingForm>(dto);
        //       details=await _repository.AddTourBookingAsync(details);
        //    return _mapper.Map<TourBookingDto>(details);
        //}
        public async Task<TourBookingDto> AddTourBookingAsync(TourBookingDto dto)
        {
            var entity = _mapper.Map<TourBookingForm>(dto);
            entity.Id = Guid.NewGuid(); // create new Id
            entity = await _repository.AddTourBookingAsync(entity);
            return _mapper.Map<TourBookingDto>(entity);
        }

        public async Task<IEnumerable<TourBookingDto>> GetAllTourBookingsAsync()
        {
            var entities = await _repository.GetAllTourBookingsAsync();
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
        public async Task<PartialTourBookingDto?> PatchTourBookingAsync(Guid id, UpdateTourBookingDto dto)
        {
            // Fetch the booking from the database
            var booking = await _repository.GetTourBookingByIdAsync(id);
            if (booking == null) return null;

            // Update only provided fields
            if (!string.IsNullOrEmpty(dto.FirstName))
                booking.FirstName = dto.FirstName;

            if (!string.IsNullOrEmpty(dto.LastName))
                booking.LastName = dto.LastName;

            if (!string.IsNullOrEmpty(dto.Gender))
                booking.Gender = dto.Gender;

            if (!string.IsNullOrEmpty(dto.Citizenship))
                booking.Citizenship = dto.Citizenship;

            if (dto.LeadPassenger.HasValue)
                booking.LeadPassenger = dto.LeadPassenger.Value;

            if (!string.IsNullOrEmpty(dto.PlaceOfBirth))
                booking.PlaceOfBirth = dto.PlaceOfBirth;

            // ParticipantType is enum; only update if it differs
            booking.ParticipantType = dto.ParticipantType;

            // Save changes
            await _context.SaveChangesAsync();

            // Map updated entity to Partial DTO
            var result = new PartialTourBookingDto
            {
                Id = booking.Id,
                FirstName = booking.FirstName,
                LastName = booking.LastName,
                Gender = booking.Gender ?? string.Empty,
                Citizenship = booking.Citizenship ?? string.Empty,
                LeadPassenger = booking.LeadPassenger.Value,
                ParticipantType = booking.ParticipantType
            };

            return result;
        }
        


        //public async Task<PartialTourBookingDto?> PatchTourBookingAsync(Guid id, UpdateTourBookingDto dto)
        //{
        //var booking = await _repository.GetTourBookingByIdAsync(id);
        //if (booking == null) return null;

        //    // Only update fields if they are provided (non-null)
        //    if (!string.IsNullOrEmpty(dto.FirstName))
        //        booking.FirstName = dto.FirstName;

        //    if (!string.IsNullOrEmpty(dto.LastName))
        //        booking.LastName = dto.LastName;

        //    if (!string.IsNullOrEmpty(dto.Gender))
        //        booking.Gender = dto.Gender;

        //    if (!string.IsNullOrEmpty(dto.Citizenship))
        //        booking.Citizenship = dto.Citizenship;

        //    if (dto.LeadPassenger.HasValue)
        //        booking.LeadPassenger = dto.LeadPassenger.Value;

        //    // ParticipantType is required, always update
        //    booking.ParticipantType = dto.ParticipantType;

        //    var updated = await _repository.UpdateTourBookingAsync(booking);

        //    return MapToPartialDto(updated);
        //}

        //private static PartialTourBookingDto MapToPartialDto(TourBookingForm booking)
        //{
        //    return new PartialTourBookingDto
        //    {
        //        FirstName = booking.FirstName,
        //        LastName = booking.LastName,
        //        Gender = booking.Gender,
        //        Citizenship = booking.Citizenship,
        //        LeadPassenger= booking.LeadPassenger.Value,
        //        ParticipantType = booking.ParticipantType
        //    };
        //}

        // 1️⃣ Fetch booking by ID
        //var booking = await _repository.GetTourBookingByIdAsync(id);
        //if (booking == null) return null; // Not found

        // 2️⃣ Apply partial updates only for non-null values
        //if (dto.FirstName != null)
        //    booking.FirstName = dto.FirstName;

        //if (dto.LastName != null)
        //    booking.LastName = dto.LastName;

        //if (dto.Gender != null)
        //    booking.Gender = dto.Gender;

        //if (dto.Citizenship != null)
        //    booking.Citizenship = dto.Citizenship;

        //if (dto.LeadPassenger.HasValue)
        //    booking.LeadPassenger = dto.LeadPassenger.Value;

        //booking.ParticipantType = dto.ParticipantType;


        //3️⃣ Save changes to DB
        //await _repository.UpdateTourBookingAsync(booking);

        //4️⃣ Map Entity → DTO before returning
        //    return new PartialTourBookingDto
        //    {
        //        Id = booking.Id,
        //        FirstName = booking.FirstName,
        //        LastName = booking.LastName,
        //        Gender = booking.Gender,
        //        Citizenship = booking.Citizenship,
        //    LeadPassenger = booking.LeadPassenger.Value,
        //            ParticipantType = booking.ParticipantType
        //};



        //var existing = await _repository.GetTourBookingByIdAsync(id);
        //if (existing == null) return null;

        //_mapper.Map(dto, existing); // Maps only the changed properties
        //var updated = await _repository.UpdateTourBookingAsync(existing);
        ////return _mapper.Map<PartialTourBookingDto>(dto);
        //return _mapper.Map<PartialTourBookingDto>(updated);

        //return _mapper.Map<TourBookingDto>(updated);




        //public async Task<TourBookingDto?> UpdateTourBookingAsync(Guid id, TourBookingDto dto)
        //{
        //    var existing = await _repository.GetTourBookingByIdAsync(id);
        //    if (existing == null) return null;

        //    // Map dto values onto existing entity
        //    _mapper.Map(dto, existing);
        //    var updated = await _repository.UpdateTourBookingAsync(existing);
        //    return _mapper.Map<TourBookingDto>(updated);
        //}
        public async Task<UpdateTourBookingDto> UpdateTourBookingAsync(Guid id, UpdateTourBookingDto dto)
        {
            var booking = await _repository.GetTourBookingByIdAsync(id);
            if (booking == null) return null;

            // 🔑 FULL UPDATE (overwrite every field)
            booking.FirstName = dto.FirstName;
            booking.LastName = dto.LastName;
            booking.Gender = dto.Gender;
            booking.Citizenship = dto.Citizenship;
            //booking.PassportNumber = dto.PassportNumber;
            //booking.IssueDate = dto.IssueDate;
            //booking.ExpiryDate = dto.ExpiryDate;
            booking.ParticipantType= dto.ParticipantType;
            booking.LeadPassenger= dto.LeadPassenger;
            booking.PlaceOfBirth = dto.PlaceOfBirth;

            var updated = await _repository.UpdateTourBookingAsync(booking);
                  

              return new UpdateTourBookingDto
            {
                //Id = entity.Id,
                //TourId = entity.TourId,
                FirstName = updated.FirstName,
                LastName =updated.LastName,
                Gender = updated.Gender,
                Citizenship = updated.Citizenship,
                //PassportNumber = entity.PassportNumber,
                //IssueDate = entity.IssueDate,
                //ExpiryDate = entity.ExpiryDate,
                LeadPassenger = updated.LeadPassenger,
                ParticipantType = updated.ParticipantType,
                PlaceOfBirth = updated.PlaceOfBirth
            };
        }
        public async Task<bool> DeleteTourBookingAsync(Guid id)
            => await _repository.DeleteTourBookingAsync(id);
    }
}



