using Domain.Enum;

namespace TourBooking.API.User.RequestObjects
{
    public class PatchUserRequest
    {
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; }=null;
        public string? Gender { get; set; } = null;
        public DateOnly? DateOfBirth { get; set; } = null;  // nullable
        public UserRole? Role { get; set; } = null;         // nullable enum
        public string? UserName { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? TelephoneNo { get; set; } = null;
        public string? Password { get; set; } = null;

    }
}
