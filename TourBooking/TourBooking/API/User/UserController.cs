using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Controllers;
using Domain.Models;
using Domain.Services.User.Interface;
using Domain.Services.User.DTO;
using AutoMapper;
using TourBooking.API.User.RequestObjects;
using System.Data;

namespace TourBooking.API.User
{
 
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpPost("Registration")]
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
        [Authorize(Roles = "AGENCY")]
        [HttpPost("AddConsultant")]
        public async Task<IActionResult> AddConsultant([FromBody] AddUserRequest request)
        {
            try
            {
         
                var dto = _mapper.Map<AddUserDto>(request);
                var result = await _userService.AddConsultantAsync(dto);

                var response = _mapper.Map<UserResponse>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var dto = _mapper.Map<LoginDto>(request);
            var token = await _userService.LoginAsync(dto);
            if (token == null) return Unauthorized("Invalid credentials");

            return Ok(new { token });
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var dto = _mapper.Map<ForgotPasswordDto>(request);
            var token = await _userService.ForgotPasswordAsync(dto);

            if (string.IsNullOrEmpty(token))
                return NotFound("User not found with this email.");

            // Optionally return token in body (client uses it in next step)
            return Ok(new
            {
                Message = "Verification code has been sent to your email.",
                VerificationToken = token
            });
        }

        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequest request)
        {
            var dto = _mapper.Map<VerifyCodeDto>(request);
            var jwtToken = await _userService.VerifyCodeAsync(dto);

            if (string.IsNullOrEmpty(jwtToken))
                return BadRequest("Invalid or expired verification code.");

            return Ok(new { Token = jwtToken });
        }
        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var dto = _mapper.Map<ResetPasswordDto>(request);
            var result = await _userService.ResetPasswordAsync(dto);
            if (!result) return BadRequest("Invalid or expired token.");
            return Ok("Password has been reset successfully.");
        }

        [Authorize(Roles = "AGENCY")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            var response = _mapper.Map<IEnumerable<UserResponse>>(users);

            return Ok(response);
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();
            var response = _mapper.Map<UserResponse>(user);
            return Ok(response);
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(Guid userId, [FromBody] AddUserRequest request)
        {
            var dto = _mapper.Map<AddUserDto>(request);
            var updated = await _userService.UpdateUserAsync(userId, dto);

            if (updated == null) return NotFound();

            var response = _mapper.Map<UserResponse>(updated);
            return Ok(response);
        }
        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(Guid id, [FromBody] PatchUserRequest request)
        {
            var PatchUserRequest = _mapper.Map<PatchUserDto>(request);
            var updated = await _userService.PatchUserAsync(id, PatchUserRequest);
            if (updated == null) return NotFound();
            return Ok(updated);
        }


        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            var deleted = await _userService.DeleteUserAsync(userId);
            if (!deleted) return NotFound();

            return Ok("User Deleted Successfully");
        }


        [Authorize(Roles = "AGENCY,CONSULTANT")]
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var users = await _userService.GetAllCustomersAsync();
            var response = _mapper.Map<IEnumerable<UserResponse>>(users);

            return Ok(response);
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpGet("LoggedUser")]
        public async Task<IActionResult> GetLoggedUser()
        {
            try
            {
                // Extract userId from JWT claims
                var userId = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userId)) return Unauthorized("User ID not found in token.");

                var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));
                if (user == null) return NotFound("User not found.");

                var response = _mapper.Map<UserResponse>(user);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }

}

