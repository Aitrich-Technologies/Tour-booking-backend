using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.User.DTO
{
    public class PatchUserDto
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public UserRole? Role { get; set; }
        public string? UserName { get; set; } 
        public string? Email { get; set; }
        public string? TelephoneNo { get; set; }

    }
}
