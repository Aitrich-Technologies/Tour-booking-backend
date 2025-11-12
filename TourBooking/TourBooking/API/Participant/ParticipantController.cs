using AutoMapper;
using Domain.Services.Participant.DTO;
using Domain.Services.Participant.Interface;
using Domain.Services.ParticipantsEditRequests;
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
        private readonly IParticipantEditRequestRepository _participantEditRequestRepository;

        public ParticipantController(
            IParticipantService service, IMapper mapper,
            IParticipantEditRequestRepository participantEditRequestRepository)
        {
            _service = service;
            _mapper = mapper;
                  _participantEditRequestRepository = participantEditRequestRepository;
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

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpPut("{bookingId}/{id}")]
        public async Task<IActionResult> UpdateParticipant(Guid bookingId, Guid id, [FromBody] UpdateParticipantRequest request)
        {
            var participant = await _service.GetParticipantByIdAsync(id);
            if (participant == null) return NotFound();

            _mapper.Map(request, participant);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = Guid.Parse(User.FindFirst("UserId")!.Value);
            if (role == "CUSTOMER")
            {
                if (participant.IsEditAllowed)
                {
                    // ✅ Allow direct update
                    var updatedParticipant = await _service.UpdateParticipantAsync(id, participant);

                    // After successful edit, disable further edits until re-approved
                    updatedParticipant.IsEditAllowed = false;
                    await _service.UpdateParticipantAsync(id, updatedParticipant);

                    return Ok(updatedParticipant);
                }
                else
                {
                    // ❌ No direct edit allowed — create approval request
                    var success = await _service.RequestParticipantEditAsync(id, participant, userId);
                    if (!success) return BadRequest("Failed to submit edit request.");
                    return Ok(new { message = "Edit request submitted for approval." });
                }
            }


            // AGENCY or CONSULTANT can update directly
            var updated = await _service.UpdateParticipantAsync(id, participant);
            return Ok(updated);
        }
        [Authorize(Roles = "AGENCY,CONSULTANT")]
        [HttpPost("approve-edit/{requestId}")]
        public async Task<IActionResult> ApproveParticipantEdit(Guid requestId, [FromQuery] bool approve, [FromBody] string? comments)
        {
            var result = await _service.ApproveEditRequestAsync(requestId, approve, comments);
            if (!result) return BadRequest("Approval failed.");
            return Ok(new { message = approve ? "Edit approved and applied." : "Edit request rejected." });
        }

        [Authorize(Roles = "AGENCY,CONSULTANT")]
        [HttpGet("pending-edits")]
        public async Task<IActionResult> GetPendingEditRequests()
        {
            var requests = await _participantEditRequestRepository.GetPendingRequestsAsync();
            return Ok(requests);
        }

    }
}
