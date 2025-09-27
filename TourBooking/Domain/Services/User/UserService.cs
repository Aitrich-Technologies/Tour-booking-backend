using Domain.Services.User.Interface;
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
using Domain.Services.User.DTO;
using Azure.Identity;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Domain.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Azure.Core;


namespace Domain.Services.User
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> AddUserAsync(AddUserDto dto)
        {
            var existingUser = await _userRepository.GetByUserNameOrEmailAsync(dto.UserName, dto.Email);
            if (existingUser != null)
            {
                throw new Exception("User with the same username or email already exists.");
                return null;
            }
            var user = _mapper.Map<AuthUser>(dto);
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            var saved = await _userRepository.AddUserAsync(user);
            return _mapper.Map<UserResponseDto>(saved);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var username = dto.UserName;
            var password = dto.Password;
            var user = await _userRepository.LoginAsync(username, password);
            if (user == null) return null;

            // Generate JWT Token
            var claims = new[]
     {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
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
            existingUser.Role = user.Role;
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
            if (request.Role != null) existing.Role = request.Role;
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
    }
}
