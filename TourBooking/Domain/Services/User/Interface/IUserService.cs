using Domain.Models;
using Domain.Services.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.User.Interface
{
    public interface IUserService
    {
        Task<UserResponseDto> AddUserAsync(AddUserDto user);
        Task<string> LoginAsync(LoginDto user);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<IEnumerable<UserResponseDto>> GetAllCustomersAsync();
        Task<UserResponseDto> GetUserByIdAsync(Guid userId);
        Task<UserResponseDto> UpdateUserAsync(Guid userId, AddUserDto user);
        Task<UserResponseDto> PatchUserAsync(Guid userId, PatchUserDto user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
