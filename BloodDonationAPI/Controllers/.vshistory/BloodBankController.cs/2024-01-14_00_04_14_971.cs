using AutoMapper;
using BloodDonationAPI.Data;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodBankController : Controller
    {
        private readonly iDonorDbContext dbContext;
        private readonly IMapper _mapper;

        public BloodBankController(iDonorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<BloodBank> Get()
        {
            return dbContext.BloodBank.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> AddBloodBank(BloodBank bloodBank2)
        {
/*            var newBank = _mapper.Map<BloodBank>(bloodBank2);
            dbContext.BloodBank.Add(newBank);
            await dbContext.SaveChangesAsync();
            return Created($"/{newBank.BloodBankID}", newBank);*/
            var bloodBank = new BloodBank()
            {
                BloodBankID = bloodBank2.BloodBankID,
                Name = bloodBank2.Name,
                Address = bloodBank2.Address,
            };

            await dbContext.BloodBank.AddAsync(bloodBank);
            await dbContext.SaveChangesAsync();

            return Ok(bloodBank);
        }
    }
}
