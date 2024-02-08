using BloodDonationAPI.Data;
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

    }
}
