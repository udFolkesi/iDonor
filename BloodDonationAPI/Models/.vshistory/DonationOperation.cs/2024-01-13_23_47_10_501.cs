using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public BloodBank BloodBank { get; set; } = null!;

        [ForeignKey("DonorID")]
        public int DonorID { get; set; }
        [JsonIgnore]
        public Client Donor { get; set; } = null!;

        [ForeignKey("PatientID")]
        public int? PatientID { get; set; }
        [JsonIgnore]
        public Client? Patient { get; set; }
    }
}
