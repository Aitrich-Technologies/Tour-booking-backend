using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Controllers;
using Domain.Models;
using Domain.Services.User.Interface;
using Domain.Services.User.DTO;
using AutoMapper;
using TourBooking.API.User.RequestObjects;

namespace TourBooking.API.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            try
            {
                var dto = _mapper.Map<AddUserDto>(request);
                var result = await _userService.AddUserAsync(dto);

                var response = _mapper.Map<UserResponse>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var dto = _mapper.Map<LoginDto>(request);
            var token = await _userService.LoginAsync(dto);
            if (token == null) return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            var response = _mapper.Map<IEnumerable<UserResponse>>(users);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();
            var response = _mapper.Map<UserResponse>(user);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(Guid userId, [FromBody] AddUserRequest request)
        {
            var dto = _mapper.Map<AddUserDto>(request);
            var updated = await _userService.UpdateUserAsync(userId, dto);

            if (updated == null) return NotFound();

            var response = _mapper.Map<UserResponse>(updated);
            return Ok(response);
        }
        //[Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(Guid id, [FromBody] PatchUserRequest request)
        {
            var PatchUserRequest = _mapper.Map<PatchUserDto>(request);
            var updated = await _userService.PatchUserAsync(id, PatchUserRequest);
            if (updated == null) return NotFound();
            return Ok(updated);
        }


        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var deleted = await _userService.DeleteUserAsync(userId);
            if (!deleted) return NotFound();

            return Ok("User Deleted Successfully");
        }
    }

}

