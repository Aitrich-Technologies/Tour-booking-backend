namespace TourBooking.API.User.RequestObjects
{
    public class VerifyCodeRequest
    {
        public string Email { get; set; }          // The user's email
        public string Code { get; set; }           // The verification code sent via email
        public string VerificationToken { get; set; } // The token that was sent with the code
    }

}
