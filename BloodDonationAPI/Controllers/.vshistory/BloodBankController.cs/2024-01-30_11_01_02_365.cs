using AutoMapper;
using BloodDonationAPI.Data;
using BloodDonationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BloodDonationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BloodBankController : Controller
    {
        private readonly iDonorDbContext dbContext;
        private readonly IMapper _mapper;

        public BloodBankController(iDonorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<BloodBank> GetBanks()
        {
            var list = dbContext.BloodBank
                .Include(b => b.DonationOperations)
                .ToList();

            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };

            return list;
        }

        [HttpPut]
        [Route("{bankID:int}")]
        public async Task<IActionResult> UpdateBank([FromRoute] int bankID, [FromBody] User updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (bankID != updatedUser.UserID)
                return BadRequest(ModelState);

            dbContext.Entry(updatedUser).State = EntityState.Modified;
            //await dbContext.User.UpdateAsync(updatedUser);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dbContext.User.Any(e => e.UserID == bankID))
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

        //[Authorize]
        [HttpPost]
        [Authorize(Roles = "Admin")]
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

        [HttpDelete("{bankId}")]
        [Authorize(Roles = "Admin")]
        public async Task DeleteBloodBank(int bankId)
        {
            /*if (!await dbContext.BloodBank.AnyAsync(b => b.BloodBankID == bankId))
            {
                return NotFound();
            }*/

            var bankToDelete = await dbContext.BloodBank.FindAsync(bankId);

/*            if (!ModelState.IsValid)
                return BadRequest(ModelState);*/

            /*            if (dbContext.BloodBank.Remove(bankToDelete))
                        {
                            ModelState.AddModelError("", "Something went wrong deleting category");
                        }*/

            dbContext.BloodBank.Remove(bankToDelete);
            await dbContext.SaveChangesAsync();
            //return NoContent();
        }
    }
}
