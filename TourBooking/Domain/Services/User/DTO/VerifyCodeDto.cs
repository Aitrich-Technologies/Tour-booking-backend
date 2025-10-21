using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.User.DTO
{
    public class VerifyCodeDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string VerificationToken { get; set; }
    }
}
