using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TourBooking.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseApiController<T> : ControllerBase
    {
    }
}
