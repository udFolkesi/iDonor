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
        public async Task<IActionResult> AddDonationOperation([FromBody]DonationOperation donationOperation)
        {

            await dbContext.DonationOperation.AddAsync(donationOperation);
            await dbContext.SaveChangesAsync();

            return Ok(donationOperation);
        }
    }
}
