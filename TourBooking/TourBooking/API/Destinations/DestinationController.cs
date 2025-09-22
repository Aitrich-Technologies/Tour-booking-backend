using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Services.Destinations.DTO;
using Domain.Services.Destinations.Interface;
using TourBooking.API.Destination.RequestObjects;
using TourBooking.API.Destinations.RequestObjects;
using Domain.Services.Destinations;
using Microsoft.AspNetCore.Http;



namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DestinationController : ControllerBase
    {
        private readonly IDestinationService _service;

        public DestinationController(IDestinationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDestinations()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDestinationById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DestinationResponseDto>> PostDestination([FromForm] DestinationDto dto)
        {
            var result = await _service.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDestination(Guid id, [FromForm] DestinationDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return Ok(new { Id = id, dto.Name, dto.City });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchDestination(Guid id, [FromForm] DestinationPatchDto dto)
        {
            var success = await _service.PatchAsync(id, dto);
            if (!success) return NotFound();
            return Ok(new { message = "Destination updated successfully" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestination(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Destination deleted successfully" });
        }


    }
}
