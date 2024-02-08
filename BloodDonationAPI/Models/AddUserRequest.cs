using Microsoft.EntityFrameworkCore;

namespace BloodDonationAPI.Models
{
    public class AddUserRequest
    {
        public string? Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
