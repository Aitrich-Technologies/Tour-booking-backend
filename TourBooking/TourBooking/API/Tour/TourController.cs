using AutoMapper;
using Domain.Services.Tour.DTO;
using Domain.Services.Tour.Interface;
using Microsoft.AspNetCore.Mvc;
using TourBooking.API.Tour.RequestObjects;
using TourBooking.Controllers;

namespace TourBooking.API.Tour
{
   
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TourController : BaseApiController<TourController>
    {
        private readonly ITourService _tourService;
        private readonly IMapper _mapper;

        public TourController(ITourService tourService, IMapper mapper)
        {
            _tourService = tourService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? destination, [FromQuery] string? status, [FromQuery] DateOnly? departureDate)
        {
            var tours = await _tourService.GetAllToursAsync(destination, status, departureDate);
            return Ok(tours);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tour = await _tourService.GetTourByIdAsync(id);
            return tour != null ? Ok(tour) : NotFound();
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetByCustomer(Guid customerId)
        {
            var tours = await _tourService.GetToursByCustomerAsync(customerId);
            return Ok(tours);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTourRequest request)
        {
            var dto = _mapper.Map<TourDto>(request);
            var created = await _tourService.CreateTourAsync(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTourRequest request)
        {
            if (id != request.Id) return BadRequest("Id mismatch");

            var dto = _mapper.Map<TourDto>(request);
            await _tourService.UpdateTourAsync(dto);
            return Ok(new { message = "Tour Updated successfully" });
        }

        [HttpPatch("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTourStatusRequest request)
        {
            var dto = _mapper.Map<UpdateTourStatusDto>(request);
            await _tourService.UpdateTourStatusAsync(id, dto);
            return Ok(new { message = "Status Changed" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tourService.DeleteTourAsync(id);
            return Ok(new { message = "Tour Deleted successfully" });
        }
    }
}
