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
using Domain.Services.User.Interface;
using Domain.Models;


namespace Domain.Services.User
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<AuthUser> AddUserAsync(AuthUser user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _userRepository.LoginAsync(username, password);
            if (user == null) return null;

            // Generate JWT Token
            var claims = new[]
     {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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

        public async Task<IEnumerable<AuthUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<AuthUser> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<AuthUser> UpdateUserAsync(Guid userId, AuthUser user)
        {
            var existing = await _userRepository.GetUserByIdAsync(userId);
            if (existing == null) return null;

            existing.FirstName = user.FirstName;
            existing.LastName = user.LastName;
            existing.Gender = user.Gender;
            existing.DateOfBirth = user.DateOfBirth;
            existing.Role = user.Role;
            existing.UserName = user.UserName;
            existing.Email = user.Email;
            existing.TelephoneNo = user.TelephoneNo;

            return await _userRepository.UpdateUserAsync(existing);
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }
    }
}
