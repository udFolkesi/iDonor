using BloodDonationAPI.Data;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationOperationController : Controller
    {
        private readonly iDonorDbContext dbContext;

        public DonationOperationController(iDonorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddDonationOperation(DonationOperation donationOperation)
        {
            /*            var newBank = _mapper.Map<BloodBank>(bloodBank2);
                        dbContext.BloodBank.Add(newBank);
                        await dbContext.SaveChangesAsync();
                        return Created($"/{newBank.BloodBankID}", newBank);*/
            var bloodBank = new BloodBank()
            {
                BloodBankID = donationOperation.BloodBankID,
                Name = donationOperation.Name,
                Address = donationOperation.Address,
            };

            await dbContext.BloodBank.AddAsync(bloodBank);
            await dbContext.SaveChangesAsync();

            return Ok(bloodBank);
        }
    }
}
