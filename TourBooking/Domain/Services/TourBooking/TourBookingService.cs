using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Domain.Services.Participant.DTO;
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
     
        public async Task<TourBookingDto> AddTourBookingAsync(TourBookingDto dto)
        {
            // Map DTO -> Entity
            var entity = new TourBookingForm
            {
                Id = Guid.NewGuid(),
                TourId = dto.TourId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                Dob = dto.Dob,
                Citizenship = dto.Citizenship,
                PassportNumber = dto.PassportNumber,
                IssueDate = dto.IssueDate,
                ExpiryDate = dto.ExpiryDate,
                PlaceOfBirth = dto.PlaceOfBirth,
                LeadPassenger = dto.LeadPassenger,
                ParticipantType = dto.ParticipantType,
                Status = dto.Status
            };

            var saved = await _repository.AddTourBookingAsync(entity);

            // Map Entity -> DTO for returning
            return new TourBookingDto
            {
                Id = saved.Id,
                TourId = saved.TourId,
                FirstName = saved.FirstName,
                LastName = saved.LastName,
                Gender = saved.Gender,
                Dob = saved.Dob,
                Citizenship = saved.Citizenship,
                PassportNumber = saved.PassportNumber,
                IssueDate = saved.IssueDate,
                ExpiryDate = saved.ExpiryDate,
                PlaceOfBirth = saved.PlaceOfBirth,
                LeadPassenger = saved.LeadPassenger,
                ParticipantType = saved.ParticipantType,
                Status = saved.Status
            };
        }
      
        public async Task<IEnumerable<TourBookingDto>> GetAllTourBookingsAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new TourBookingDto
            {
                Id = e.Id,
                TourId = e.TourId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Gender = e.Gender,
                Dob = e.Dob,
                Citizenship = e.Citizenship,
                PassportNumber = e.PassportNumber,
                IssueDate = e.IssueDate,
                ExpiryDate = e.ExpiryDate,
                PlaceOfBirth = e.PlaceOfBirth,
                LeadPassenger = e.LeadPassenger,
                ParticipantType = e.ParticipantType,
                Status = e.Status
            });
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
        public async Task<TourBookingDto?>UpdateTourBookingAsync(Guid id, UpdateTourBookingDto dto)

        {
            var entity = await _repository.GetTourBookingByIdAsync(id);
            if (entity == null) return null;

            // Full replace
            entity.TourId = dto.TourId;
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Gender = dto.Gender;
            entity.Dob = dto.Dob;
            entity.Citizenship = dto.Citizenship;
            entity.PassportNumber = dto.PassportNumber;
            entity.IssueDate = dto.IssueDate;
            entity.ExpiryDate = dto.ExpiryDate;
            entity.PlaceOfBirth = dto.PlaceOfBirth;
            entity.LeadPassenger = dto.LeadPassenger;
            entity.ParticipantType = dto.ParticipantType;
            entity.Status = dto.Status;

            var saved = await _repository.UpdateAsync(entity);
            return MapToDto(saved);
        }

        public async Task<TourBookingDto?> PatchTourBookingAsync(Guid id, PatchTourBookingDto dto)
        
        {
            var entity = await _repository.GetTourBookingByIdAsync(id);
            if (entity == null) return null;

            // Only update provided fields
            if (dto.TourId.HasValue) entity.TourId = dto.TourId.Value;
            if (!string.IsNullOrEmpty(dto.FirstName)) entity.FirstName = dto.FirstName;
            if (!string.IsNullOrEmpty(dto.LastName)) entity.LastName = dto.LastName;
            if (!string.IsNullOrEmpty(dto.Gender)) entity.Gender = dto.Gender;
            if (dto.Dob.HasValue) entity.Dob = dto.Dob;
            if (!string.IsNullOrEmpty(dto.Citizenship)) entity.Citizenship = dto.Citizenship;
            if (!string.IsNullOrEmpty(dto.PassportNumber)) entity.PassportNumber = dto.PassportNumber;
            if (dto.IssueDate.HasValue) entity.IssueDate = dto.IssueDate;
            if (dto.ExpiryDate.HasValue) entity.ExpiryDate = dto.ExpiryDate;
            if (!string.IsNullOrEmpty(dto.PlaceOfBirth)) entity.PlaceOfBirth = dto.PlaceOfBirth;
            if (dto.LeadPassenger.HasValue) entity.LeadPassenger = dto.LeadPassenger;
            if (dto.ParticipantType.HasValue) entity.ParticipantType = dto.ParticipantType.Value;
            if (dto.Status.HasValue) entity.Status = dto.Status.Value;

            var saved = await _repository.UpdateAsync(entity);
            return MapToDto(saved);
        }

        private static TourBookingDto MapToDto(TourBookingForm e) => new()
        {
            Id = e.Id,
            TourId = e.TourId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Gender = e.Gender,
            Dob = e.Dob,
            Citizenship = e.Citizenship,
            PassportNumber = e.PassportNumber,
            IssueDate = e.IssueDate,
            ExpiryDate = e.ExpiryDate,
            PlaceOfBirth = e.PlaceOfBirth,
            LeadPassenger = e.LeadPassenger,
            ParticipantType = e.ParticipantType,
            Status = e.Status
        };
       
        public async Task<bool> DeleteTourBookingAsync(Guid id)
            => await _repository.DeleteTourBookingAsync(id);
    }
}



