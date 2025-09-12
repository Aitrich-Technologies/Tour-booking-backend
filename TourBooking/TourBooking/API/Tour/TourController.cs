using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Controllers;

namespace TourBooking.API.Tour
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : BaseApiController<TourController>
    {
    }
}
