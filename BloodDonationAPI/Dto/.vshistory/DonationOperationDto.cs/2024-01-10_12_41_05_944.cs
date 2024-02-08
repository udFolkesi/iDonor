using BloodDonationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonationAPI.Dto
{
    public class DonationOperationDto
    {
        public int DonationOperationID { get; set; }
        public DateTime CollectionTime { get; set; }
        public int Volume { get; set; }
        public DateTime? TransfusionTime { get; set; }
        public string Status { get; set; } = "";
        public DateTime ExpiryTime { get; set; }
    }
}
