using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Services.User.Interface
{
    public interface IUserRepository
    {
        Task<AuthUser> AddUserAsync(AuthUser user);
        Task<AuthUser> LoginAsync(string username, string password);
        Task<IEnumerable<AuthUser>> GetAllUsersAsync();
        Task<AuthUser> GetUserByIdAsync(Guid userId);
        Task<AuthUser> UpdateUserAsync(AuthUser user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
