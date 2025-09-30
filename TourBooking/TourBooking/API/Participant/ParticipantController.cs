using AutoMapper;
using Domain.Services.Participant.DTO;
using Domain.Services.Participant.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.API.Participant.RequestObjects;
using TourBooking.Controllers;

namespace TourBooking.API.Participant
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ParticipantController : BaseApiController<ParticipantController>
    {
        private readonly IParticipantService _service;
        private readonly IMapper _mapper;

        public ParticipantController(IParticipantService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetParticipants(Guid bookingId)
        {
            var result = await _service.GetParticipantsAsync(bookingId);
            return Ok(result);
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpGet("{bookingId}/{id}")]
        public async Task<IActionResult> GetParticipantById(Guid bookingId, Guid id)
        {
            var result = await _service.GetParticipantByIdAsync(bookingId, id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpPost("{bookingId}")]
        public async Task<IActionResult> AddParticipant(Guid bookingId, [FromBody] AddParticipantRequest request)
        {
            var dto = _mapper.Map<ParticipantDto>(request);
            var result = await _service.AddParticipantAsync(bookingId, dto);

            return CreatedAtAction(nameof(GetParticipantById),
                new { bookingId, id = result.Id },
                result);
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpPut("{bookingId}/{id}")]
        public async Task<IActionResult> UpdateParticipant(Guid bookingId, Guid id, [FromBody] UpdateParticipantRequest request)
        {
            var dto = _mapper.Map<ParticipantDto>(request);
            dto.Id = id;
            dto.BookingId = bookingId;

            var result = await _service.UpdateParticipantAsync(bookingId, id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpPatch("{bookingId}/{id}")]
        public async Task<IActionResult> PatchParticipant(Guid bookingId, Guid id, [FromBody] PatchParticipantRequest request)
        {
            var participant = await _service.GetParticipantByIdAsync(bookingId, id);
            if (participant == null) return NotFound();

            // Map only provided fields
            _mapper.Map(request, participant);

            var updated = await _service.UpdateParticipantAsync(bookingId, id, participant);
            return Ok(updated);
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpDelete("{bookingId}/{id}")]
        public async Task<IActionResult> DeleteParticipant(Guid bookingId, Guid id)
        {
            var deleted = await _service.DeleteParticipantAsync(bookingId, id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Participant deleted successfully", id });
        }
    }
}
