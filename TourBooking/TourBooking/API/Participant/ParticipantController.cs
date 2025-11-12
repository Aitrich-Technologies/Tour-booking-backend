using AutoMapper;
using Domain.Services.Participant.DTO;
using Domain.Services.Participant.Interface;
using Domain.Services.TourBooking.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        private readonly ITourBookingService _tourBookingService;

        public ParticipantController(IParticipantService service, IMapper mapper, ITourBookingService tourBookingService)
        {
            _service = service;
            _mapper = mapper;
            _tourBookingService = tourBookingService;
        }

        // ✅ Get all participants of a booking
        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetParticipantsByBookingId(Guid bookingId)
        {
            var result = await _service.GetParticipantsAsync(bookingId);
            return Ok(result);
        }

        // ✅ Get a single participant
        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetParticipantById(Guid id)
        {
            var result = await _service.GetParticipantByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // ✅ Add participant
        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpPost("{bookingId}")]
        public async Task<IActionResult> AddParticipant(Guid bookingId, [FromBody] AddParticipantRequest request)
        {
            var userIdString = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized("UserId claim missing.");

            request.LeadId = Guid.Parse(userIdString);

            var dto = _mapper.Map<ParticipantDto>(request);
            dto.BookingId = bookingId;

            var result = await _service.AddParticipantAsync(bookingId, dto);

            return CreatedAtAction(nameof(GetParticipantById),
                new { id = result.Id }, result);
        }

        // ✅ Delete participant
        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(Guid id)
        {
            var deleted = await _service.DeleteParticipantAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Participant deleted successfully", id });
        }

        // ✅ Update participant (with CUSTOMER edit request logic)
        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpPut("{bookingId}/{id}")]
        public async Task<IActionResult> UpdateParticipant(Guid bookingId, Guid id, [FromBody] UpdateParticipantRequest request)
        {
            var participant = await _service.GetParticipantByIdAsync(id);
            if (participant == null) return NotFound();

            _mapper.Map(request, participant);

            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            // CUSTOMER logic
            if (role == "CUSTOMER")
            {
                var booking = await _tourBookingService.GetTourBookingByIdAsync(bookingId);

                if (booking.IsEditAllowed)
                {
                    var updatedParticipant = await _service.UpdateParticipantAsync(id, participant);
                    return Ok(updatedParticipant);
                }

                // Request Edit Approval
                var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
                await _tourBookingService.RequestEditAsync(bookingId, userId, "Customer requested edit.");

                return Ok(new { message = "Edit request sent to agency for approval." });

            }

            // AGENCY or CONSULTANT can update directly
            var updated = await _service.UpdateParticipantAsync(id, participant);
            return Ok(updated);
        }
    }
}
