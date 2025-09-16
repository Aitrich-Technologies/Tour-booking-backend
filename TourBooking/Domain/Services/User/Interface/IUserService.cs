using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.User.Interface
{
    public interface IUserService
    {
        Task<AuthUser> AddUserAsync(AuthUser user);
        Task<string> LoginAsync(string username, string password);
        Task<IEnumerable<AuthUser>> GetAllUsersAsync();
        Task<AuthUser> GetUserByIdAsync(Guid userId);
        Task<AuthUser> UpdateUserAsync(Guid userId, AuthUser user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
