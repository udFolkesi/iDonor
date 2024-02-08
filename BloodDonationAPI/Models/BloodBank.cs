using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonationAPI.Models
{
    [PrimaryKey("BloodBankID")]
    public class BloodBank
    {
        public int BloodBankID { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        //[NotMapped]
        public ICollection<DonationOperation> DonationOperations { get; } = new List<DonationOperation>();
        // public DateTime WorkingHours { get; set; }
    }
}
