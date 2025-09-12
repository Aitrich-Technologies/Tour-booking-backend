using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TourBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController<T> : ControllerBase
    {
    }
}
