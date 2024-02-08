using BloodDonationAPI.Data;
using BloodDonationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationAPI.Business.Services
{
    public class OperationService
    {
        private readonly iDonorDbContext _context;

        public OperationService(iDonorDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DonationOperation> Search(int bloodGroup, string users)
        {
            if(bloodGroup > 0 && bloodGroup < 5)
            {
                if (users == "Donors")
                {
                    return _context.DonationOperation.Where(d => d.Donor.Blood.BloodGroup == bloodGroup)
                        .Include(d => d.Donor)
                        .ToList();
                }
                else if (users == "Patients")
                {
                    return _context.DonationOperation.Where(d => d.Patient.Blood.BloodGroup == bloodGroup)
                        .Include(d => d.Patient)
                        .ToList();
                }
            }

            return null;
        }

        public IEnumerable<DonationOperation> Sorting(string order, string users)
        {
            if (users == "Donors")
            {
                return _context.DonationOperation.OrderBy(d => d).ToList();
            }
            else if (users == "Patients")
            {
                return _context.DonationOperation.OrderByDescending(d => d).ToList();
            }

            return null;
        }
    }
}
