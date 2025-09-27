using Domain.Services.Terms.DTO;
using Domain.Services.Terms.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.API.Terms.RequestObjects;
using TourBooking.Controllers;

namespace TourBooking.API.Terms
{
  
    [ApiController]
    [Route("api/v1/Tour")]
    public class TermsController : BaseApiController<TermsController>
    {
        private readonly ITermsAndConditionService _service;



        public TermsController(ITermsAndConditionService service)
        {
            _service = service;
        }

        [HttpGet("{tourId:guid}/terms")]
        public async Task<IActionResult> GetTermsByTourIdAsync([FromRoute] Guid tourId)
        {
            var result = await _service.GetTermsByTourIdAsync(tourId);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTermByIdAsync(Guid id)
        {
            var term = await _service.GetTermByIdAsync(id);
            if (term == null) return NotFound();
            return Ok(term);
        }
        [HttpPost("{tourId}/terms")]
        public async Task<IActionResult> AddTermAsync([FromRoute] Guid tourId, [FromBody] AddTerms request)
        {
            if (request == null)
                return BadRequest("Request body is required.");

            // ✅ Check if a term already exists for this TourId
            var existingTerms = await _service.GetTermsByTourIdAsync(tourId);
            if (existingTerms != null && existingTerms.Any())
            {
                return BadRequest(new { message = "A term already exists for this tour." });
            }

            var newTerm = new TermsDto
            {
                Id = Guid.NewGuid(),
                TourId = tourId,
                Terms = request.Terms
            };

            var result = await _service.AddTermAsync(newTerm);
            return Ok(result);
        }





        // ✅ Update existing term
        [HttpPut("tourId/terms/{id:guid}")]
        public async Task<IActionResult> UpdateTermAsync([FromRoute] Guid id, [FromBody] UpdateTerms request)
        {
            if (request == null)
                return BadRequest("Request body is required.");

            var existing = await _service.GetTermByIdAsync(id);
            if (existing == null) return NotFound();

            // Keep a copy of the old term for response
            var oldTerm = new TermsDto
            {
                Id = existing.Id,
                Terms = existing.Terms
            };

            // Update with new values
            var updatedTerm = new TermsDto
            {
                Id = id,
                Terms = request.Terms
            };

            var updated = await _service.UpdateTermAsync(id, updatedTerm);

            // Return both old and new in response
            return Ok(new
            {
                Old = oldTerm,
                Updated = updated
            });
        }

        [HttpDelete("tourId/terms/{id:guid}")]
        public async Task<IActionResult> DeleteTermAsync([FromRoute] Guid id)
        {
            var existing = await _service.GetTermByIdAsync(id);
            if (existing == null) return NotFound(new { message = "Term not found." });

            await _service.DeleteTermAsync(id);

            // ✅ return a message instead of NoContent
            Console.WriteLine("Returning: Term deleted successfully.");
            return Ok(new { message = "Term deleted successfully." });

        }

    }
}


