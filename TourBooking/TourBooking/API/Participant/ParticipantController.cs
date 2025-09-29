using Domain.Services.Participant.DTO;
using Domain.Services.Participant.Interface;

using Microsoft.AspNetCore.Mvc;
using TourBooking.API.Participant.RequestObjects;
using TourBooking.Controllers;

using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
namespace TourBooking.API.Participant
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ParticipantController : BaseApiController<ParticipantController>
    {
        private readonly IParticipantService _service;

        public ParticipantController(IParticipantService service)
        {
            _service = service;
        }
        //[Authorize(Roles="CONSULTANT")]
        [HttpGet("{bookingId}")]
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

        [HttpPost("{bookingId}")]
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


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchParticipant(Guid bookingId, Guid id, [FromBody] PatchParticipantRequest request)
        {
            var participant = await _service.GetParticipantByIdAsync(bookingId, id);
            if (participant == null) return NotFound();

            // Update only if request provides a new value
            if (request.FirstName != null) participant.FirstName = request.FirstName;
            if (request.LastName != null) participant.LastName = request.LastName;
            if (request.Gender != null) participant.Gender = request.Gender;
            if (request.Citizenship != null) participant.Citizenship = request.Citizenship;
            if (request.PassportNumber != null) participant.PassportNumber = request.PassportNumber;
            if (request.IssueDate != null) participant.IssueDate = request.IssueDate;
            if (request.ExpiryDate != null) participant.ExpiryDate = request.ExpiryDate;
            if (request.PlaceOfBirth != null) participant.PlaceOfBirth = request.PlaceOfBirth;

            var updated = await _service.UpdateParticipantAsync(bookingId, id, participant);

            // Return full participant (all fields, including unchanged ones)
            return Ok(updated);
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
