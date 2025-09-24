using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Services.User.Interface;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services.User
{
    internal class UserRepository : IUserRepository
    {
        private readonly TourBookingDbContext _context;

        public UserRepository(TourBookingDbContext context)
        {
            _context = context;
        }

        public async Task<AuthUser> AddUserAsync(AuthUser user)
        {
            _context.AuthUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<AuthUser> LoginAsync(string username, string password)
        {
            return await _context.AuthUsers
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }

        public async Task<IEnumerable<AuthUser>> GetAllUsersAsync()
        {
            return await _context.AuthUsers.ToListAsync();
        }

        public async Task<AuthUser?> GetByUserNameOrEmailAsync(string userName, string email)
        {
            return await _context.AuthUsers
                .FirstOrDefaultAsync(u => u.UserName == userName || u.Email == email);
        }
        public async Task<AuthUser> GetUserByIdAsync(Guid userId)
        {
            return await _context.AuthUsers.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<AuthUser> UpdateUserAsync(AuthUser user)
        {
            _context.AuthUsers.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var user = await _context.AuthUsers.FindAsync(userId);
            if (user == null) return false;

            _context.AuthUsers.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    }
