using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Controllers;
using Domain.Models;
using Domain.Services.User.Interface;
using Domain.Services.User.DTO;

namespace TourBooking.API.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto dto)
        {
            var user = new AuthUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Role = dto.Role,
                UserName = dto.UserName,
                Email = dto.Email,
                TelephoneNo = dto.TelephoneNo,
                Password = dto.Password
            };

            var result = await _userService.AddUserAsync(user);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _userService.LoginAsync(dto.UserName, dto.Password);
            if (token == null) return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(Guid userId, [FromBody] AddUserDto dto)
        {
            var user = new AuthUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                Role = dto.Role,
                UserName = dto.UserName,
                Email = dto.Email,
                TelephoneNo = dto.TelephoneNo,
                Password = dto.Password
            };

            var updated = await _userService.UpdateUserAsync(userId, user);
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

