using AutoMapper;
using Domain.Models;
using Domain.Services.Email.Helper;
using Domain.Services.Email.Interface;
using Domain.Services.Participant.DTO;
using Domain.Services.Participant.Interface;
using Domain.Services.ParticipantsEditRequests;
using Domain.Services.TourBooking.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Services.Participant
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly ITourBookingService _bookingService;
        private readonly IParticipantEditRequestRepository _editRequestRepo;


        public ParticipantService(
            IParticipantRepository repository, 
            IMapper mapper,IMailService mailService,
            ITourBookingService tourBookingService,
            IParticipantEditRequestRepository participantEditRequest)
        {
            _repository = repository;
            _mapper = mapper;
            _mailService = mailService;
            _bookingService = tourBookingService;
            _editRequestRepo = participantEditRequest;
        }

        public async Task<IEnumerable<ParticipantDto>> GetParticipantsAsync(Guid bookingId)
        {
            var entities = await _repository.GetParticipantsAsync(bookingId);
            return _mapper.Map<IEnumerable<ParticipantDto>>(entities);
        }

        public async Task<ParticipantDto?> GetParticipantByIdAsync(Guid id)
        {
            var entity = await _repository.GetParticipantByIdAsync(id);
            return _mapper.Map<ParticipantDto>(entity);
        }

        public async Task<ParticipantDto> AddParticipantAsync(Guid bookingId, ParticipantDto dto)
        {
            // Map DTO to entity
            var entity = _mapper.Map<ParticipantInformation>(dto);
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


        public async Task<ParticipantDto?> UpdateParticipantAsync (Guid id, ParticipantDto dto)
        {
            var entity = await _repository.GetParticipantByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            

            await _repository.UpdateParticipantAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ParticipantDto>(entity);
        }

        public async Task<bool> DeleteParticipantAsync(Guid id)
        {
            var entity = await _repository.GetParticipantByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteParticipantAsync(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RequestParticipantEditAsync(Guid participantId, ParticipantDto dto, Guid requestedBy)
        {
            var existing = await _repository.GetParticipantByIdAsync(participantId);
            if (existing == null) return false;

            // Convert the new values to JSON for review
            var updatedJson = System.Text.Json.JsonSerializer.Serialize(dto);

            var editRequest = new ParticipantEditRequest
            {
                ParticipantId = participantId,
                RequestedBy = requestedBy,
                UpdatedDataJson = updatedJson,
                Status = "PENDING"
            };

            await _editRequestRepo.AddAsync(editRequest);
            await _editRequestRepo.SaveChangesAsync();

            
            return true;
        }
        public async Task<bool> ApproveEditRequestAsync(Guid requestId, bool approve, string? comments = null)
        {
            var request = await _editRequestRepo.GetByIdAsync(requestId);
            if (request == null) return false;

            var participant = await _repository.GetParticipantByIdAsync(request.ParticipantId);
            if (participant == null) return false;

            if (approve)
            {
                // Deserialize the requested updates
                var dto = JsonSerializer.Deserialize<ParticipantDto>(request.UpdatedDataJson!);

                // Apply the approved changes
                _mapper.Map(dto, participant);

                // ✅ Allow future edits after approval
                participant.IsEditAllowed = true;

                await _repository.UpdateParticipantAsync(participant);
                request.Status = "APPROVED";
            }
            else
            {
                request.Status = "REJECTED";
                participant.IsEditAllowed = false;
            }

            request.Comments = comments;
            request.ReviewedAt = DateTime.UtcNow;

            await _editRequestRepo.UpdateAsync(request);
            await _repository.SaveChangesAsync();
            await _editRequestRepo.SaveChangesAsync();

            return true;
        }

    }

}
