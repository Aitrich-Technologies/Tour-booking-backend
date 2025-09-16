namespace TourBooking.API.User.RequestObjects
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
