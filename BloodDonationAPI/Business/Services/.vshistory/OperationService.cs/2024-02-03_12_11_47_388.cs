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
            if(users == "Donors")
            {
                return _context.DonationOperation.Where(d => d.Donor.Blood.BloodGroup == bloodGroup).ToList();
            }
            else if(users == "Patients")
            {
                return _context.DonationOperation.Where(d => d.Patient.Blood.BloodGroup == bloodGroup).ToList();
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<DonationOperation> Sorting(int bloodGroup)
        {
            /*            if (users == "Donors")
                        {
                            return _context.DonationOperation.Where(d => d.Donor.Blood.BloodGroup == bloodGroup).ToList();
                        }
                        else if (users == "Patients")
                        {
                            return _context.DonationOperation.Where(d => d.Patient.Blood.BloodGroup == bloodGroup).ToList();
                        }
                        else
                        {
                            return null;
                        }*/
            return null;
        }


    }
}
