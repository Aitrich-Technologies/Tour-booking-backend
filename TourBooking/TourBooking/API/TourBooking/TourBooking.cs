using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Controllers;

namespace TourBooking.API.TourBooking
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourBooking : BaseApiController<TourBooking>
    {
    }
}
