
using Domain.Services.Notifications.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TourBooking.API.Controllers
{
    [Authorize(Roles = "AGENCY,CONSULTANT")]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetByRole(string role)
        {
            var result = await _service.GetNotificationsForRoleAsync(role);
            return Ok(result);
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            await _service.MarkAsReadAsync(id);
            return NoContent();
        }
    }
}
