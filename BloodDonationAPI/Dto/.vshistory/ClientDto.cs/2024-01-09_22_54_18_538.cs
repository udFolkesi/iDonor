using BloodDonationAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonationAPI.Dto
{
    public class ClientDto
    {
        public int ClientID { get; set; }
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Gender { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; } = "";
        public int UserID { get; set; }
        public int BloodID { get; set; }
    }
}
