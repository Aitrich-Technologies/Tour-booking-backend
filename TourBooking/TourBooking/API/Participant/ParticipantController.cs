using Domain.Services.Participant.DTO;
using Domain.Services.Participant.Interface;
using Microsoft.AspNetCore.Mvc;
using TourBooking.API.Participant.RequestObjects;
<<<<<<< HEAD
using TourBooking.Controllers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
=======

>>>>>>> 13c5b6126e0674dde3e9af550c03a6f6092bade8
namespace TourBooking.API.Participant
{
    [ApiController]
    [Route("api/v1/TourBooking/{bookingId}/Participant")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _service;

        public ParticipantController(IParticipantService service)
        {
            _service = service;
        }
        [Authorize(Roles="CONSULTANT")]
        [HttpGet]
        public async Task<IActionResult> GetParticipants(Guid bookingId)
        {
            var result = await _service.GetParticipantsAsync(bookingId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipantById(Guid bookingId, Guid id)
        {
            var result = await _service.GetParticipantByIdAsync(bookingId, id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant(Guid bookingId, [FromBody] AddParticipantRequest request)
        {
            var dto = new ParticipantDto
            {
                LeadId = request.LeadId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Citizenship = request.Citizenship,
                PassportNumber = request.PassportNumber,
                IssueDate = request.IssueDate,
                ExpiryDate = request.ExpiryDate,
                PlaceOfBirth = request.PlaceOfBirth
            };

            var result = await _service.AddParticipantAsync(bookingId, dto);
            return CreatedAtAction(nameof(GetParticipantById), new { bookingId, id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipant(Guid bookingId, Guid id, [FromBody] UpdateParticipantRequest request)
        {
            var dto = new ParticipantDto
            {
                Id = id,
                BookingId = bookingId,
                LeadId = bookingId,//to be changed
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Citizenship = request.Citizenship,
                PassportNumber = request.PassportNumber,
                IssueDate = request.IssueDate,
                ExpiryDate = request.ExpiryDate,
                PlaceOfBirth = request.PlaceOfBirth
            };

            var result = await _service.UpdateParticipantAsync(bookingId, id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(Guid bookingId, Guid id)
        {
            var deleted = await _service.DeleteParticipantAsync(bookingId, id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Participant deleted successfully", id });
        }
    }
}
