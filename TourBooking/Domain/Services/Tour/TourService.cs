using AutoMapper;
using Domain.Models;
using Domain.Services.Email.Helper;
using Domain.Services.Email.Interface;
using Domain.Services.Tour.DTO;

using Domain.Services.Tour.Interface;
using System.Security.Claims;


namespace Domain.Services.Tour.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public TourService(ITourRepository repository, IMapper mapper,IMailService mailService)
        {
            _repository = repository;
            _mapper = mapper;
            _mailService = mailService;
        }
        public async Task<List<TourDto>> GetToursAsync()
        {
      
            var tours = await _repository.GetAllAsync();
            var toursWithoutCustomer = tours
                .Where(t => t.CustomerId == null)
                .ToList();
            return _mapper.Map<List<TourDto>>(toursWithoutCustomer);

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

        //public async Task UpdateTourStatusAsync(Guid tourId, UpdateTourStatusDto statusDto)
        //{
        //    await _repository.UpdateStatusAsync(tourId, statusDto.Status.ToString());
        //}
        public async Task UpdateTourStatusAsync(Guid tourId, UpdateTourStatusDto statusDto)
        {
            var tour = await _repository.GetByIdAsync(tourId);
            if (tour == null) return;

            var oldStatus = tour.Status.ToString();
            var newStatus = statusDto.Status.ToString();

            await _repository.UpdateStatusAsync(tourId, newStatus);

            // ✅ Only send if status changed
            if (oldStatus == newStatus) return;

            // ✅ Collect valid email recipients
            var recipientEmails = new List<string>();

            // Lead Customer Email
            if (IsValidEmail(tour.Customer?.Email))
                recipientEmails.Add(tour.Customer.Email);

            // Participant Emails (from TourBookings)
            if (tour.TourBookingForms != null)
            {
                foreach (var booking in tour.TourBookingForms)
                {
                    if (booking.ParticipantInformations != null)
                    {
                        foreach (var participant in booking.ParticipantInformations)
                        {
                            if (IsValidEmail(participant.Email))
                                recipientEmails.Add(participant.Email);
                        }
                    }
                }
            }

            // Remove duplicates
            recipientEmails = recipientEmails.Distinct().ToList();

            if (!recipientEmails.Any()) return;

            // ✅ Email Content
            string subject = $"Tour Status Update - {tour.TourName}";
            string body = $@"
        <h2>Tour Status Updated</h2>

        <p>The status of the tour <strong>{tour.TourName}</strong> has been updated.</p>

        <p><strong>Previous Status:</strong> {oldStatus}<br/>
        <strong>New Status:</strong> {newStatus}</p>

        <p>Departure: {tour.DepartureDate?.ToString("dd MMM yyyy")}<br/>
        Arrival: {tour.ArrivalDate?.ToString("dd MMM yyyy")}</p>

        <br/>
        <p>Thank you,</p>
        <p><strong>Travel Support Team</strong></p>";

            // ✅ Send email to each participant individually
            foreach (var emailAddress in recipientEmails)
            {
                var emailRequest = new MailRequest
                {
                    ToEmail = emailAddress,
                    Subject = subject,
                    Body = body
                };

                await _mailService.SendEmailAsync(emailRequest);
            }
        }

        private bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public async Task DeleteTourAsync(Guid tourId)
        {
            await _repository.DeleteAsync(tourId);
        }

    }
}
