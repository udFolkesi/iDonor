using BloodDonationAPI.Data;
using BloodDonationAPI.Models;

namespace BloodDonationAPI
{
    public class Seed
    {
        private readonly iDonorDbContext dbContext;
        public Seed(iDonorDbContext context)
        {
            dbContext = context;
        }

        public void SeedDataContext()
        {
            if (!dbContext.Blood.Any())
            {
                var bloodTypes = new List<Blood>()
                {

                };
            }
        }
    }
}
