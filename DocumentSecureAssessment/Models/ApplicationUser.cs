using Microsoft.AspNetCore.Identity;



namespace DocumentSecureAssessment.Models
{
    public class ApplicationUser:IdentityUser
    {

    }
    // RegisterModel.cs
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // LoginModel.cs
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
