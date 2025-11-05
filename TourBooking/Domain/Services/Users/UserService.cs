using Domain.Services.Users.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using AutoMapper;
using Domain.Services.Users.DTO;
using Azure.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Azure.Core;
using Domain.Enums;
using System.Net.Mail;
using System.Net;
using Domain.Services.Email.Helper;
using Domain.Services.Email.Interface;


namespace Domain.Services.Users
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration config,IMailService mailService)
        {
            _userRepository = userRepository;
            _config = config;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<UserResponseDto> AddUserAsync(AddUserDto dto)
        {
            var existingUser = await _userRepository.GetByUserNameOrEmailAsync(dto.UserName, dto.Email);
            if (existingUser != null)
            {
                throw new Exception("User with the same username or email already exists.");
               
            }
            
            var user = _mapper.Map<AuthUser>(dto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            user.Role = UserRole.CUSTOMER;
            var saved = await _userRepository.AddUserAsync(user);
            return _mapper.Map<UserResponseDto>(saved);
        }
        public async Task<UserResponseDto> AddConsultantAsync(AddUserDto dto)
        {
            var existingUser = await _userRepository.GetByUserNameOrEmailAsync(dto.UserName, dto.Email);
            if (existingUser != null)
                throw new Exception("User with the same username or email already exists.");

            var consultant = _mapper.Map<AuthUser>(dto);
            consultant.Password = BCrypt.Net.BCrypt.HashPassword(consultant.Password);
            consultant.Id = Guid.NewGuid();
            consultant.CreatedAt = DateTime.UtcNow;
            consultant.Role = UserRole.CONSULTANT; //  consultant role

            var saved = await _userRepository.AddUserAsync(consultant);

            // ✅ Send confirmation email
            var email = new MailRequest
            {
                ToEmail = dto.Email,
                Subject = "Consultant Registration",
                Body = $@"
            <h2>Consultant Registration</h2>
            <p>Dear {dto.FirstName} {dto.LastName},</p>
            <p>We are pleased to welcome you to Lions Sports Club, Travel Agency</p>
            <p>Email:<strong>{dto.Email}</strong></p>
            <p>UserName:<strong>{dto.UserName}</strong></p>
            <p>Password:<strong>{dto.Password}</strong></p>
        
            <p>We are excited to have you with us.</p>"
            };
            await _mailService.SendEmailAsync(email);
            return _mapper.Map<UserResponseDto>(saved);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.LoginAsync(dto.UserName, dto.Password);
            if (user == null) return null;

            // Generate JWT Token using the separate method
            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(AuthUser user)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
        new Claim("UserId", user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null) return false;

            // Generate a reset token (for demo use Guid, in real use JWT or Identity's Token)
            var resetToken = GenerateJwtToken(user);

            // Save token in DB
            //user.PasswordResetToken = resetToken;
            //user.TokenExpiry = DateTime.UtcNow.AddHours(1);

            await _userRepository.UpdateUserAsync(user);

            // Send Email (or log for now)
            await SendEmailAsync(user.Email, "Password Reset",
                $"Use this token to reset your password: {resetToken}");


            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null ) return false;

            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            //user.PasswordResetToken = null;
            //user.TokenExpiry = null;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllCustomersAsync()
        {
            var user = await _userRepository.GetAllCustomersAsync();
            var customers = _mapper.Map<IEnumerable<UserResponseDto>>(user);
            
            return customers;
        }

        public async Task<UserResponseDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> UpdateUserAsync(Guid userId, AddUserDto user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId);

            if (existingUser == null)
                throw new Exception("User not found");

            // Update only the fields you need
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Gender = user.Gender;
            existingUser.DateOfBirth = user.DateOfBirth; 
            //existingUser.Role = Enum.Parse < UserRole >(user.Role);
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.TelephoneNo = user.TelephoneNo;


            var updated = await _userRepository.UpdateUserAsync(existingUser);
            var updatedUser = _mapper.Map<UserResponseDto>(updated);
            return updatedUser;
        }

        public async Task<UserResponseDto> PatchUserAsync(Guid id, PatchUserDto request)
        {
            var existing = await _userRepository.GetUserByIdAsync(id);
            if (existing == null) return null;

            if (request.FirstName != null) existing.FirstName = request.FirstName;
            if (request.LastName != null) existing.LastName = request.LastName;
            if (request.Gender != null) existing.Gender = request.Gender;
            if (request.Role != null) existing.Role = Enum.Parse<UserRole>(request.Role);
            if (request.UserName != null) existing.UserName = request.UserName;
            if (request.DateOfBirth != null) existing.DateOfBirth = request.DateOfBirth;
            if (request.TelephoneNo != null) existing.TelephoneNo = request.TelephoneNo;
            if (request.Email != null) existing.Email = request.Email;


            var updated = await _userRepository.UpdateUserAsync(existing);

            var updatedUser = _mapper.Map<UserResponseDto>(updated);
            return updatedUser;
        }
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }


        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var host = _config["Email:SmtpHost"];
                var portString = _config["Email:SmtpPort"];
                var user = _config["Email:SmtpUser"];
                var pass = _config["Email:SmtpPass"];
                var enableSslString = _config["Email:EnableSSL"];

                if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                    throw new Exception("Email configuration is missing in appsettings.json.");

                int port = 587; // default
                if (!string.IsNullOrEmpty(portString))
                    int.TryParse(portString, out port);

                bool enableSsl = true;
                if (!string.IsNullOrEmpty(enableSslString))
                    bool.TryParse(enableSslString, out enableSsl);

                using var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(user, pass),
                    EnableSsl = enableSsl
                };

                using var message = new MailMessage(user, toEmail, subject, body)
                {
                    IsBodyHtml = true
                };

                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Email send failed: {ex.Message}", ex);
            }
        }


    }
}

