using AutoMapper;
using BloodDonationAPI.Data;
using BloodDonationAPI.Dto;
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
        private readonly IMapper _mapper;

        public ClientController(iDonorDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPut]
        [Route("{clientID:int}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int clientID, [FromBody] ClientDto updatedClient)
        {
            if (updatedClient == null)
                return BadRequest(ModelState);

            if (clientID != updatedClient.ClientID)
                return BadRequest(ModelState);

            dbContext.Entry(updatedClient).State = EntityState.Modified;
            var clientMap = _mapper.Map<Client>(updatedClient);
            //await dbContext.User.UpdateAsync(updatedUser);

            try
            {
                dbContext.Update(clientMap);
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dbContext.Client.Any(e => e.ClientID == clientID))
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
