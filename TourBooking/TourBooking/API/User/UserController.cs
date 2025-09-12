using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Controllers;

namespace TourBooking.API.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController<UserController>
    {
    }
}
