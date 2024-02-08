using Microsoft.EntityFrameworkCore;

namespace BloodDonationAPI.Models
{
    [PrimaryKey("BloodID")]
    public class Blood
    {
        public int BloodID { get; set; }
        public int BloodGroup { get; set; }
        public string RhesusFactor { get; set; } = "";
    }
}
