using Domain.Models;
using Domain.Services.Tours.DTO;
using Microsoft.AspNetCore.Mvc;
using TourBooking.API.Tour.RequestObjects;
using TourBooking.Services.Tours.DTO;
using TourBooking.Services.Tours.Interface;


namespace TourBooking.API.Tour
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITourService _service;

        public TourController(ITourService service)
        {
            _service = service;
        }

        // ✅ POST: api/v1/Tour
        [HttpPost]
        public async Task<IActionResult> AddTour([FromBody] AddTourRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dto = new TourRegisterDto
            {

                TourName = request.TourName,
                TourDescription = request.TourDescription,
                DestinationId = request.DestinationId,
                NoOfNights = request.NoOfNights,
                Price = request.Price,
                DepartureDate = request.DepartureDate,
                ArrivalDate = request.ArrivalDate,
                CustomerId = request.CustomerId,   // ✅ optional
                ConsultantId = request.ConsultantId,
                Status = request.Status,
               
            };

            var created = await _service.CreateTourAsync(dto);

            return CreatedAtAction(nameof(GetTourById), new { id = created.Id }, created);
        }

       
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTourById(Guid id)
        {
            var tour = await _service.GetTourByIdAsync(id);
            if (tour == null)
                return NotFound();

            return Ok(tour);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTours()
        {
            var tours = await _service.GetAllToursAsync();
            return Ok(tours);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(Guid id)
        {
            var success = await _service.DeleteTourAsync(id);
            if (!success)
                return NotFound(new { message = "Tour not found" });

            return NoContent(); // 204
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTour(Guid id, [FromBody] UpdateTourDto dto)
        {
            var updated = await _service.PatchTourAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = "Tour not found" });

            return Ok(updated);
        }


        // PUT: api/v1/tour/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTour(Guid id, TourPutDto dto)
        {
            var updatedTour = await _service.PutTourAsync(id, dto);
            if (updatedTour == null) return NotFound();
            return Ok(updatedTour);
        }


    }
}



