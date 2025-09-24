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
        public string FirstName { get; set; } = null;
        public string LastName { get; set; }=null;
        public string Gender { get; set; } = null;
        public DateOnly? DateOfBirth { get; set; }=null;
        public UserRole? Role { get; set; } = null;  
        public string UserName { get; set; } = null;
        public string Email { get; set; } = null ;
        public string TelephoneNo { get; set; } = null;
        public string Password { get; set; } = null;

    }
}
