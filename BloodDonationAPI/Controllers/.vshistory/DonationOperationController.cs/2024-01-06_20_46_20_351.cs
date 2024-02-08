using BloodDonationAPI.Data;
using BloodDonationAPI.Dto;
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
        public async Task<IActionResult> AddDonationOperation([FromBody]DonationOperationDto operationCreate)
        {

            await dbContext.DonationOperation.AddAsync(operationCreate);
            await dbContext.SaveChangesAsync();
            
            return Ok(operationCreate);
        }
    }
}
