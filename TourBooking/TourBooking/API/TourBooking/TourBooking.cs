using AutoMapper;
using AutoMapper;
using Domain.Models;
using Domain.Services.TourBooking.DTO;
using Domain.Services.TourBooking.Interface;
using Microsoft.AspNetCore.Mvc;
using TourBooking.API.TourBooking.RequestObjects;
using TourBooking.Controllers;

namespace TourBooking.API.TourBooking
{
    [Route("api/v1/tourbookings")]
    [ApiController]
    public class TourBookingController : BaseApiController<TourBookingController>
    {
        private readonly ITourBookingService _service;
        private readonly IMapper _mapper;

        public TourBookingController(ITourBookingService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // ✅ Create
        [HttpPost]
        public async Task<IActionResult> AddTourBooking([FromBody] TourBookingDto dto)
        {
            var tourBooking = await _service.AddTourBookingAsync(dto);
            // Return 201 Created with location header pointing to GetTourBookingById
            return CreatedAtAction(
                nameof(GetTourBookingById),
                new { id = tourBooking.Id },
                tourBooking
            );
        }

        // ✅ Get All
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllTourBookingsAsync());

        // ✅ Get By Id
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTourBookingById(Guid id)
        {
            var booking = await _service.GetTourBookingByIdAsync(id);
            return booking == null ? NotFound() : Ok(booking);
        }

        // ✅ Get By TourId
        [HttpGet("by-tour/{tourId:guid}")]
        public async Task<IActionResult> GetByTourId(Guid tourId)
            => Ok(await _service.GetTourBookingsByTourIdAsync(tourId));



        //// ✅ Update
        //[HttpPut("{id:guid}")]
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] UpdateTourBookingDto dto)
        {
            var updated = await _service.UpdateTourBookingAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTourBooking(Guid id, [FromBody] UpdateTourBookingRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var dto = new UpdateTourBookingDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Citizenship = request.Citizenship,
                LeadPassenger = request.LeadPassenger,
                ParticipantType = request.ParticipantType,
                PlaceOfBirth= request.PlaceOfBirth

            };

            var updated = await _service.UpdateTourBookingAsync(id, dto);

            return updated == null
                ? NotFound(new { Message = "Booking not found." })
                : Ok(new { Message = "Tour booking updated successfully.", Data = updated });
        }
    

        // ✅ Delete
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteTourBookingAsync(id);
            if (!success) return NotFound();
            return Ok(new { message = "Booking deleted successfully" });
        }
    }
}


//[HttpPost]
//public async Task<IActionResult> CreateForm([FromBody]TourBookingDto dto)
//{
//    var tour=await _service.AddTourBookingAsync(dto);
//    return CreatedAtAction(
//       nameof(GetTourBookingById),
//       new { id = tour.TourId },
//       tour
//   );




