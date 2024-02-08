using Microsoft.EntityFrameworkCore;

namespace BloodDonationAPI.Models
{
    [PrimaryKey("UserID")]
    public class User
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public required string Name { get; set; }
        public string Surname { get; set; } = "";
        public string? Patronymic { get; set; }
        public string Role { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
