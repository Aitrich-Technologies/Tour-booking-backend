using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Users.DTO
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
