using BloodDonationAPI.Data;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClientController : Controller
    {
        private readonly iDonorDbContext dbContext;

        public ClientController(iDonorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPut]
        [Route("{userID:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userID, [FromBody] User updatedClient)
        {
            if (updatedClient == null)
                return BadRequest(ModelState);

            if (userID != updatedClient.UserID)
                return BadRequest(ModelState);

            dbContext.Entry(updatedClient).State = EntityState.Modified;
            //await dbContext.User.UpdateAsync(updatedUser);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dbContext.User.Any(e => e.UserID == userID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
