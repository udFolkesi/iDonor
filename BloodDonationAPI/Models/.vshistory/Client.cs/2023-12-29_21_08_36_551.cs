using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonationAPI.Models
{
    [PrimaryKey("ClientID")]
    public class Client
    {
        public int ClientID { get; set; }
        public string Address { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Gender { get; set; } = "";
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; } = "";

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("BloodID")]
        public int BloodID { get; set; }
        public Blood Blood { get; set; }
        [NotMapped]
        public ICollection<DonationOperation> DonationOperations { get; } = new List<DonationOperation>();
    }
}
