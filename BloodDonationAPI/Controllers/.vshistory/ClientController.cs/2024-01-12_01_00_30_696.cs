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

        [HttpGet]
        public IEnumerable<Client> GetClients()
        {
            return dbContext.Client
                .Include(c => c.Blood)
                .Include(c => c.User)
                .Include(c => c.DonationsAsDonor)
                .Include(c => c.DonationsAsPatient)
                .ToList();
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromQuery] int userId, [FromQuery] int bloodId, [FromBody] ClientDto clientCreate)
        {
/*            var bloodEntity = await dbContext.Blood.FindAsync(bloodId);
            if (bloodEntity == null)
            {
                return NotFound("Blood not found");
            }

            var userEntity = await dbContext.User.FindAsync(userId);
            if (userEntity == null)
            {
                return NotFound("User not found");
            }*/
            var clientMap = _mapper.Map<Client>(clientCreate);

            //clientMap.Blood = await dbContext.Blood.FindAsync(bloodId); 
            //clientMap.User = await dbContext.User.FindAsync(userId);

            clientMap.BloodID = bloodId;
            clientMap.UserID = userId;

            await dbContext.Client.AddAsync(clientMap);
            await dbContext.SaveChangesAsync();

            return Ok(clientCreate);
        }

        [HttpPut]
        [Route("{clientID:int}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int clientID, [FromBody] ClientDto updatedClient)
        {
            if (updatedClient == null)
                return BadRequest(ModelState);

            if (clientID != updatedClient.ClientID)
                return BadRequest(ModelState);

            try
            {
                var existingClient = await dbContext.Client.FindAsync(clientID);

                if (existingClient == null)
                {
                    return NotFound();
                }

                _mapper.Map(updatedClient, existingClient);

                dbContext.Entry(existingClient).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                return NoContent();
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
        }
    }
}
