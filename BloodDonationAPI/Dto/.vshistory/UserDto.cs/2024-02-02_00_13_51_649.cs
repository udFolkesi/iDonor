using BloodDonationAPI.Models;

namespace BloodDonationAPI.Dto
{
    public class UserDto
    {
        public string? Username { get; set; }
        public required string Name { get; set; }
        public string Surname { get; set; } = "";
        public string? Patronymic { get; set; }
        public string Role { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
