using Domain.Enums;

namespace TourBooking.API.User.RequestObjects
{
    public class AddUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public UserRole Role { get; set; }  // Customer | Consultant
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
        public string Password { get; set; }
    }
}
