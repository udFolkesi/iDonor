using BloodDonationAPI.Data;
using BloodDonationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationAPI.Business.Services
{
    public class StatisticService
    {
        private readonly iDonorDbContext _context;

        public StatisticService(iDonorDbContext context)
        {
            _context = context;
        }

        public void CountDonatedBlood()
        {
            //int bloodAmount = _context.DonationOperation.
        }
    }
}
