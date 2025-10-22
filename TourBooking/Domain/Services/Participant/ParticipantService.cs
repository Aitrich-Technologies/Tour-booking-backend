using AutoMapper;
using Domain.Models;
using Domain.Services.Email.Helper;
using Domain.Services.Email.Interface;
using Domain.Services.Participant.DTO;
using Domain.Services.Participant.Interface;
using Domain.Services.TourBooking.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Participant
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly ITourBookingService _bookingService;

        public ParticipantService(IParticipantRepository repository, IMapper mapper,IMailService mailService,ITourBookingService tourBookingService)
        {
            _repository = repository;
            _mapper = mapper;
            _mailService = mailService;
            _bookingService = tourBookingService;
        }

        public async Task<IEnumerable<ParticipantDto>> GetParticipantsAsync(Guid bookingId)
        {
            var entities = await _repository.GetParticipantsAsync(bookingId);
            return _mapper.Map<IEnumerable<ParticipantDto>>(entities);
        }

        public async Task<ParticipantDto?> GetParticipantByIdAsync(Guid bookingId, Guid id)
        {
            var entity = await _repository.GetParticipantByIdAsync(bookingId, id);
            return _mapper.Map<ParticipantDto>(entity);
        }

        public async Task<ParticipantDto> AddParticipantAsync(Guid bookingId, ParticipantDto dto)
        {
            // Map DTO to entity
            var entity = _mapper.Map<ParticipantInformation>(dto);
            entity.LeadId = bookingId;

            await _repository.AddParticipantAsync(entity);
            await _repository.SaveChangesAsync();

            var booking = await _bookingService.GetTourBookingByIdAsync(bookingId);
            

            string? tourName = booking?.Tour?.TourName;
            DateOnly? arrivalDate = booking?.Tour?.ArrivalDate;
            DateOnly? departureDate = booking?.Tour?.DepartureDate;

            // Build the email body
            var email = new MailRequest
            {
                ToEmail = dto.Email,
                Subject = "Tour Booking Confirmation",
                Body = $@"
                <h2>Booking Confirmation</h2>
                <p>Dear {dto.FirstName} {dto.LastName},</p>
                <p>Thank you for booking with us!</p>
                <p><strong>Tour Name:</strong> {tourName ?? "N/A"}</p>
                <p><strong>Departure Date:</strong> {departureDate?.ToString("dd MMM yyyy") ?? "N/A"}</p>
                <p><strong>Arrival Date:</strong> {arrivalDate?.ToString("dd MMM yyyy") ?? "N/A"}</p>
                <br/>
                <p>We look forward to having you on this journey!</p>"
                };

            // Send confirmation email
            if (!string.IsNullOrEmpty(dto.Email))
                await _mailService.SendEmailAsync(email);

            return _mapper.Map<ParticipantDto>(entity);
        }


        public async Task<ParticipantDto?> UpdateParticipantAsync(Guid bookingId, Guid id, ParticipantDto dto)
        {
            var entity = await _repository.GetParticipantByIdAsync(bookingId, id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            entity.LeadId = bookingId;

            await _repository.UpdateParticipantAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ParticipantDto>(entity);
        }

        public async Task<bool> DeleteParticipantAsync(Guid bookingId, Guid id)
        {
            var entity = await _repository.GetParticipantByIdAsync(bookingId, id);
            if (entity == null) return false;

            await _repository.DeleteParticipantAsync(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
    }

}
