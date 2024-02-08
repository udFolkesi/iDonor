using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonationAPI.Models
{
    [PrimaryKey("DonationOperationID")]
    public class DonationOperation
    {
        public int DonationOperationID { get; set; }
        public DateTime CollectionTime { get; set; }
        public int Volume { get; set; }
        public DateTime? TransfusionTime { get; set; }
        public string Status { get; set; } = "";
        public DateTime ExpiryTime { get; set; }

        [ForeignKey("BloodBankID")]
        public int BloodBankID { get; set; }
        public required BloodBank BloodBank { get; set; }

        [ForeignKey("DonorID")]
        public int DonorID { get; set; }
        public Client Donor { get; set; } = null!;

        [ForeignKey("PatientID")]
        public int PatientID { get; set; }
        public Client? Patient { get; set; }
    }
}
