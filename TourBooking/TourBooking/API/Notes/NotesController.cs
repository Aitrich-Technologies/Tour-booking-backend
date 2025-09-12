using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Controllers;

namespace TourBooking.API.Notes
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : BaseApiController<NotesController>
    {
    }
}
