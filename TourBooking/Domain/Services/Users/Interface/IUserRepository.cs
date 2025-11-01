using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.Users.Interface
{
    public interface IUserRepository
    {
        Task<AuthUser> AddUserAsync(AuthUser user);
        Task<AuthUser?> GetByUserNameOrEmailAsync(string userName, string email);

        Task<AuthUser> LoginAsync(string username, string password);
        Task<AuthUser> GetByEmailAsync(string email);
        Task<IEnumerable<AuthUser>> GetAllUsersAsync();
        Task<IEnumerable<AuthUser>> GetAllCustomersAsync();
        Task<AuthUser> GetUserByIdAsync(Guid userId);
        Task<AuthUser> UpdateUserAsync(AuthUser user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
